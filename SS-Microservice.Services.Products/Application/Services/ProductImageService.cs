using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.ProductImage.Commands;
using SS_Microservice.Services.Products.Application.Features.ProductImage.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Specification.ProductImage;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Services
{
    public class ProductImageService : IProductImageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUploadService _uploadService;

        public ProductImageService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _uploadService = uploadService;
        }

        public async Task<List<ProductImageDto>> GetListProductImage(GetListProductImageQuery query)
        {
            var productImages = await _unitOfWork.Repository<ProductImage>()
                .ListAsync(new ProductImageSpecification(query.ProductId));
            var productImageDtos = new List<ProductImageDto>();
            productImages.ForEach(productImage => productImageDtos.Add(_mapper.Map<ProductImageDto>(productImage)));

            return productImageDtos;
        }

        public async Task<ProductImageDto> GetProductImage(GetProductImageQuery query)
        {
            var productImage = await _unitOfWork.Repository<ProductImage>().GetById(query.Id)
                ?? throw new NotFoundException("Cannot find current product image");

            return _mapper.Map<ProductImageDto>(productImage);
        }

        public async Task<long> CreateProductImage(CreateProductImageCommand command)
        {
            ProductImageDto productImageDto = new()
            {
                ProductId = command.ProductId,
                Image = _uploadService.UploadFile(command.Image).Result,
                Size = command.Image.Length,
                ContentType = command.Image.ContentType
            };
            var productImage = _mapper.Map<ProductImage>(productImageDto);
            productImage.Product = await _unitOfWork.Repository<Product>().GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find current product");

            await _unitOfWork.Repository<ProductImage>().Insert(productImage);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create entity");
            }
            return command.ProductId;
        }

        public async Task<bool> UpdateProductImage(UpdateProductImageCommand command)
        {
            var productImage = await _unitOfWork.Repository<ProductImage>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current product image");

            productImage.Image = _uploadService.UploadFile(command.Image).Result;
            productImage.Size = command.Image.Length;
            productImage.ContentType = command.Image.ContentType;
            _unitOfWork.Repository<ProductImage>().Update(productImage);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> SetDefaultProductImage(SetDefaultProductImageCommand command)
        {
            var productImageDefault = await _unitOfWork.Repository<ProductImage>()
                .GetEntityWithSpec(new ProductImageSpecification(command.ProductId, true))
                ?? throw new NotFoundException("Cannot find current product image");

            var productImage = await _unitOfWork.Repository<ProductImage>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current product image");

            productImageDefault.IsDefault = false;
            _unitOfWork.Repository<ProductImage>().Update(productImageDefault);

            productImage.IsDefault = true;
            _unitOfWork.Repository<ProductImage>().Update(productImage);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteProductImage(DeleteProductImageCommand command)
        {
            var productImage = await _unitOfWork.Repository<ProductImage>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current product image");

            if (productImage.IsDefault)
            {
                throw new Exception("Cannot delete product image default");
            }
            _unitOfWork.Repository<ProductImage>().Delete(productImage);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot delete product image default");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteListProductImage(DeleteListProductImageCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var productImage = await _unitOfWork.Repository<ProductImage>().GetById(id)
                        ?? throw new NotFoundException("Cannot find current product image");

                    if (productImage.IsDefault)
                    {
                        throw new Exception("Cannot delete product image default");
                    }
                    _unitOfWork.Repository<ProductImage>().Delete(productImage);
                }
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot delete product image default");
                }
                await _unitOfWork.Commit();
                return isSuccess;
            }
            catch (Exception)
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }
    }
}