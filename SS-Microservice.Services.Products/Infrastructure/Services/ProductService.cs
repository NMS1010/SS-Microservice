using AutoMapper;
using Grpc.Core;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Common.Interfaces;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Product.Commands;
using SS_Microservice.Services.Products.Application.Product.Queries;
using SS_Microservice.Services.Products.Core.Entities;
using SS_Microservice.Services.Products.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using SS_Microservice.Common.Grpc.Product.Protos;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper, IUploadService uploadService)
        {
            _repository = repository;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task AddProduct(ProductCreateCommand command)
        {
            var product = new Product();

            var now = DateTime.Now;

            product.Status = command.Status;
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Origin = command.Origin;
            product.Quantity = command.Quantity;
            product.Id = Guid.NewGuid().ToString();
            product.DateCreated = now;
            product.DateUpdated = now;
            product.Status = 1;
            if (command.Image != null)
            {
                product.MainImage = await _uploadService.UploadFile(command.Image);
            }
            if (command.SubImages != null)
            {
                foreach (var file in command.SubImages)
                {
                    if (file != null)
                    {
                        product.Images.Add(new()
                        {
                            Id = Guid.NewGuid().ToString(),
                            ImageName = await _uploadService.UploadFile(file)
                        });
                    }
                }
            }
            await _repository.Insert(product);
        }

        public async Task<bool> DeleteProduct(ProductDeleteCommand command)
        {
            var product = await _repository.GetById(command.ProductId);
            var isDeleteSuccess = _repository.Delete(product);
            if (isDeleteSuccess)
            {
                await _uploadService.DeleteFile(product.MainImage);
                foreach (var item in product.Images)
                {
                    await _uploadService.DeleteFile(item.ImageName);
                }
            }
            return isDeleteSuccess;
        }

        public async Task<PaginatedResult<ProductDTO>> GetAllProduct(ProductGetAllQuery query)
        {
            var result = await _repository.FilterProduct(query);
            var productDtos = new List<ProductDTO>();
            foreach (var item in result.Items)
            {
                productDtos.Add(_mapper.Map<Product, ProductDTO>(item));
            }
            return new PaginatedResult<ProductDTO>(productDtos, (int)query.PageIndex, result.TotalPages, (int)query.PageSize);
        }

        public async Task<ProductDTO> GetProductById(ProductGetByIdQuery query)
        {
            var product = await _repository.GetById(query.ProductId);

            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<bool> UpdateProduct(ProductUpdateCommand command)
        {
            var product = await _repository.GetById(command.Id) ?? throw new NotFoundException("Cannot find this product");
            product.Status = command.Status;
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.Origin = command.Origin;
            product.Quantity = command.Quantity;
            product.DateUpdated = DateTime.Now;

            if (command.Image != null)
            {
                product.MainImage = await _uploadService.UploadFile(command.Image);
            }

            return _repository.Update(product);
        }

        public async Task<bool> UpdateProductImage(ProductImageUpdateCommand command)
        {
            var product = await _repository.GetById(command.ProductId) ?? throw new NotFoundException("Cannot find this product");
            if (command.Image != null)
            {
                var prdImg = product.Images.Where(x => x.Id == command.ProductImageId).FirstOrDefault();
                if (prdImg != null)
                {
                    await _uploadService.DeleteFile(prdImg.ImageName);
                    prdImg.ImageName = await _uploadService.UploadFile(command.Image);
                }
            }
            return _repository.Update(product);
        }

        public async Task<bool> DeleteProductImage(ProductImageDeleteCommand command)
        {
            var product = await _repository.GetById(command.ProductId) ?? throw new NotFoundException("Cannot find this product");
            var productImg = product.Images.Where(x => x.Id == command.ProductImageId).FirstOrDefault() ?? throw new NotFoundException("Cannot find this product image");
            await _uploadService.DeleteFile(productImg.ImageName);
            var isRemoveSuccess = product.Images.Remove(productImg);
            return _repository.Update(product) && isRemoveSuccess;
        }

        public override async Task<ProductResponse> GetProductInformation(GetProductDetail request, ServerCallContext context)
        {
            var product = await _repository.GetById(request.ProductId);
            var productDto = _mapper.Map<ProductDTO>(product);
            return new ProductResponse()
            {
                Description = productDto.Description,
                MainImage = productDto.MainImage,
                Name = productDto.Name,
                Origin = productDto.Origin,
                Price = (double)productDto.Price,
                Quantity = productDto.Quantity,
            };
        }
    }
}