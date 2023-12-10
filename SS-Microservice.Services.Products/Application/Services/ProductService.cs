using AutoMapper;
using Grpc.Core;
using Newtonsoft.Json.Linq;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Common.Enums;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Messaging.Commands.Inventory;
using SS_Microservice.Services.Products.Application.Model.Variant;
using SS_Microservice.Services.Products.Application.Specification.Product;
using SS_Microservice.Services.Products.Application.Specification.Variant;
using SS_Microservice.Services.Products.Domain.Entities;

namespace SS_Microservice.Services.Products.Application.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IUploadService uploadService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _uploadService = uploadService;
            _unitOfWork = unitOfWork;
        }

        public override async Task<ProductCustomGrpcResponse> GetProductInformation(GetProductByVariant request, ServerCallContext context)
        {
            var variant = await _unitOfWork.Repository<Variant>().GetEntityWithSpec(new VariantSpecification(request.VariantId))
                ?? throw new InvalidRequestException("Unexpected variantId");

            var product = variant.Product;
            if (product == null)
                return null;

            var isPromotion = variant.PromotionalItemPrice.HasValue;

            var productDto = _mapper.Map<ProductDto>(product);

            return new ProductCustomGrpcResponse()
            {
                VariantQuantity = variant.Quantity,
                ProductId = productDto.Id,
                ProductName = productDto.Name,
                ProductImage = productDto?.Images.FirstOrDefault(x => x.IsDefault)?.Image ?? product.Images.FirstOrDefault()?.Image,
                ProductSlug = productDto.Slug,
                ProductUnit = productDto.Unit.Name,
                ProductQuantity = productDto.Quantity,
                Sku = variant.Sku,
                TotalPrice = (double)(variant.Quantity * variant.ItemPrice),
                TotalPromotionalPrice = isPromotion ? variant.Quantity * (double)variant.PromotionalItemPrice.Value : null,
                VariantName = variant.Name,
                VariantId = variant.Id,
                VariantPrice = (double)variant.ItemPrice,
                VariantPromotionalPrice = isPromotion ? (double)variant.PromotionalItemPrice.Value : null,
                IsPromotion = isPromotion,
                ProductActualQuantity = productDto.ActualInventory,
                Status = productDto.Status
            };
        }

        public async Task<PaginatedResult<ProductDto>> GetListProduct(GetListProductQuery query)
        {
            var spec = new ProductSpecification(query, isPaging: true);
            var countSpec = new ProductSpecification(query);
            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Product>().CountAsync(countSpec);
            var productDtos = new List<ProductDto>();
            products.ForEach(product =>
            {
                var productDto = _mapper.Map<ProductDto>(product);
                productDtos.Add(productDto);
            });

            return new PaginatedResult<ProductDto>(productDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<ProductDto> GetProduct(GetProductQuery query)
        {
            var spec = new ProductSpecification(query.Id);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec)
                ?? throw new NotFoundException("Cannot find current product");

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }

        public async Task<long> CreateProduct(CreateProductCommand command)
        {
            var product = _mapper.Map<Product>(command);
            product.Category = await _unitOfWork.Repository<Category>().GetById(command.CategoryId)
                ?? throw new NotFoundException("Cannot find current product category");

            product.Brand = await _unitOfWork.Repository<Brand>().GetById(command.BrandId)
                ?? throw new NotFoundException("Cannot find current brand");

            product.Unit = await _unitOfWork.Repository<Unit>().GetById(command.UnitId)
                ?? throw new NotFoundException("Cannot find current unit");

            product.Quantity = 0;
            product.ActualInventory = 0;
            product.Sold = 0;
            product.Rating = 5;

            if (command.SaleId != null)
            {
                product.Sale = await _unitOfWork.Repository<Sale>().GetById(command.SaleId)
                    ?? throw new NotFoundException("Cannot find current sale");
            }
            product.Status = PRODUCT_STATUS.INACTIVE;

            List<ProductImage> productImages = new();
            foreach (IFormFile image in command.ProductImages)
            {
                ProductImage productImage = new()
                {
                    Image = _uploadService.UploadFile(image).Result,
                    Size = image.Length,
                    ContentType = image.ContentType
                };
                productImages.Add(productImage);
            }
            productImages[0].IsDefault = true;
            product.Images = productImages;

            List<Variant> variants = new();
            foreach (string v in command.Variants)
            {
                var variant = _mapper.Map<Variant>(JObject.Parse(v).ToObject<CreateVariantRequest>());
                variant.Status = VARIANT_STATUS.ACTIVE;
                variants.Add(variant);
            }
            product.Variants = variants;

            await _unitOfWork.Repository<Product>().Insert(product);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create entity");
            }

            return product.Id;
        }

        public async Task<bool> UpdateProduct(UpdateProductCommand command)
        {
            var product = await _unitOfWork.Repository<Product>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current product");

            product = _mapper.Map(command, product);
            product.Id = command.Id;

            product.Category = await _unitOfWork.Repository<Category>().GetById(command.CategoryId)
                ?? throw new NotFoundException("Cannot find current product category");

            product.Brand = await _unitOfWork.Repository<Brand>().GetById(command.BrandId)
                ?? throw new NotFoundException("Cannot find current product brand");

            product.Unit = await _unitOfWork.Repository<Unit>().GetById(command.UnitId)
                ?? throw new NotFoundException("Cannot find current unit");

            if (command.SaleId != null)
            {
                product.Sale = await _unitOfWork.Repository<Sale>().GetById(command.SaleId)
                    ?? throw new NotFoundException("Cannot find current sale");
            }

            product.Status = product.Status switch
            {
                PRODUCT_STATUS.ACTIVE => PRODUCT_STATUS.ACTIVE,
                PRODUCT_STATUS.INACTIVE => PRODUCT_STATUS.INACTIVE,
                PRODUCT_STATUS.SOLD_OUT => PRODUCT_STATUS.SOLD_OUT,
                _ => throw new InvalidRequestException("Unexpected product status: " + command.Status),
            };
            _unitOfWork.Repository<Product>().Update(product);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteProduct(DeleteProductCommand command)
        {
            var product = await _unitOfWork.Repository<Product>().GetById(command.Id)
                ?? throw new NotFoundException("Cannot find current product");

            product.Status = PRODUCT_STATUS.INACTIVE;
            _unitOfWork.Repository<Product>().Update(product);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update status of entity");
            }

            return isSuccess;
        }

        public async Task<bool> DeleteListProduct(DeleteListProductCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();

                foreach (var id in command.Ids)
                {
                    var product = await _unitOfWork.Repository<Product>().GetById(id)
                        ?? throw new NotFoundException("Cannot find current product");

                    product.Status = PRODUCT_STATUS.INACTIVE;
                    _unitOfWork.Repository<Product>().Update(product);
                }
                var isSuccess = await _unitOfWork.Save() > 0;
                if (!isSuccess)
                {
                    throw new Exception("Cannot update status of entities");
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

        public async Task<ProductDto> GetProductBySlug(GetProductBySlugQuery query)
        {
            var spec = new ProductSpecification(query.Slug);
            var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(spec)
                ?? throw new NotFoundException("Cannot find current product");

            var productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }


        public async Task<PaginatedResult<ProductDto>> GetListFilteringProduct(GetListFilteringProductQuery query)
        {
            var spec = new ProductFilterSpecification(query, isPaging: true);
            var countSpec = new ProductFilterSpecification(query);

            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Product>().CountAsync(countSpec);

            var productDtos = products.Select(x => _mapper.Map<ProductDto>(x)).ToList();

            return new PaginatedResult<ProductDto>(productDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<PaginatedResult<ProductDto>> GetListSearchingProduct(GetListSearchingProductQuery query)
        {
            var spec = new ProductSearchSpecification(query, isPaging: true);
            var countSpec = new ProductSearchSpecification(query);

            var products = await _unitOfWork.Repository<Product>().ListAsync(spec);
            var count = await _unitOfWork.Repository<Product>().CountAsync(countSpec);

            var productDtos = products.Select(x => _mapper.Map<ProductDto>(x)).ToList();

            return new PaginatedResult<ProductDto>(productDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task UpdateOneProductQuantity(UpdateOneProductQuantityCommand command)
        {
            var product = await _unitOfWork.Repository<Product>().GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find product");

            product.Quantity += command.Quantity;
            product.ActualInventory = command.ActualInventory;

            _unitOfWork.Repository<Product>().Update(product);
            await _unitOfWork.Save();
        }

        public async Task<bool> UpdateListProductQuantity(UpdateListProductQuantityCommand command)
        {
            return true;
            //return await _repository.UpdateProductQuantity(command);
        }
    }
}