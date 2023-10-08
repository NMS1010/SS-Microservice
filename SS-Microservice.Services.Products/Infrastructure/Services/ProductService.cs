using AutoMapper;
using Grpc.Core;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Domain.Entities;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Common.StringUtil;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Application.Common.Constants;
using Microsoft.AspNetCore.Server.IISIntegration;
using DnsClient;
using MediatR;
using System.Xml.Linq;
using SS_Microservice.Services.Products.Application.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class ProductService : ProductProtoService.ProductProtoServiceBase, IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        private readonly IUploadService _uploadService;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper, IUploadService uploadService, ICategoryRepository categoryRepository, IBrandRepository brandRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _uploadService = uploadService;
            _categoryRepository = categoryRepository;
            _brandRepository = brandRepository;
        }

        public async Task<bool> AddProduct(CreateProductCommand command)
        {
            var product = new Product
            {
                Name = command.Name,
                Description = command.Description,
                Status = PRODUCT_STATUS.ACTIVE_PRODUCT,
                BrandId = command.BrandId,
                Code = command.Code,
                Unit = command.Unit,
                ShortDescription = command.ShortDescription,
                Sold = 0,
                Rating = 0,
                Quantity = command.Quantity,
                Id = Guid.NewGuid().ToString(),
                Slug = command.Name.Slugify(),
                CategoryId = command.CategoryId,
                SaleId = command.SaleId,
                Cost = command.Cost,
            };

            if (command.Image != null)
            {
                var prdImg = new ProductImage()
                {
                    Id = Guid.NewGuid().ToString(),
                    Image = await _uploadService.UploadFile(command.Image),
                    ContentType = command.Image.ContentType,
                    Size = command.Image.Length,
                    IsDefault = true
                };
                UtilMethod.SetAuditable(DateTime.Now, prdImg);
                product.Images.Add(prdImg);
            }
            if (command.SubImages != null)
            {
                foreach (var file in command.SubImages)
                {
                    if (file != null)
                    {
                        var prdImg = new ProductImage()
                        {
                            Id = Guid.NewGuid().ToString(),
                            Image = await _uploadService.UploadFile(file),
                            ContentType = file.ContentType,
                            Size = file.Length,
                            IsDefault = false
                        };
                        UtilMethod.SetAuditable(DateTime.Now, prdImg);
                        product.Images.Add(prdImg);
                    }
                }
            }
            //if (command.Variants != null)
            //{
            //    for (int i = 0; i < command.Variants.Count; i++)
            //    {
            //        var variantCreateRequest = command.Variants[i];
            //        if (variantCreateRequest != null)
            //        {
            //            var variant = new Variant()
            //            {
            //                Id = Guid.NewGuid().ToString(),
            //                Sku = $"{product.Code}-{i}",
            //                ItemCost = variantCreateRequest.ItemCost,
            //                ItemPrice = variantCreateRequest.ItemPrice,
            //                Quantity = variantCreateRequest.Quantity,
            //                Name = variantCreateRequest.Name,
            //                TotalPrice = variantCreateRequest.Quantity * variantCreateRequest.ItemPrice,
            //                PromotionalItemPrice = variantCreateRequest.ItemPrice,
            //                Status = PRODUCT_STATUS.ACTIVE_PRODUCT
            //            };
            //            UtilMethod.SetAuditable(DateTime.UtcNow, variant);
            //            product.Variants.Add(variant);
            //        }
            //    }
            //}
            return await _repository.Insert(product);
        }

        public async Task<bool> DeleteProduct(DeleteProductCommand command)
        {
            var product = await _repository.GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot update product status");
            product.Status = PRODUCT_STATUS.INACTIVE_PRODUCT;
            var isDeleteSuccess = _repository.Update(product);
            return isDeleteSuccess;
        }

        public async Task<PaginatedResult<ProductDto>> GetAllProduct(GetAllProductQuery query)
        {
            var result = await _repository.FilterProduct(query);
            var productDtos = new List<ProductDto>();
            foreach (var item in result.Items)
            {
                var productDto = _mapper.Map<ProductDto>(item);
                var category = await _categoryRepository.GetById(item.CategoryId)
                    ?? throw new NotFoundException("Cannot find product category");

                var brand = await _brandRepository.GetById(item.BrandId)
                    ?? throw new NotFoundException("Cannot find product brand");
                productDto.Category = _mapper.Map<CategoryDto>(category);
                productDto.Brand = _mapper.Map<BrandDto>(brand);
                productDtos.Add(productDto);
            }
            return new PaginatedResult<ProductDto>(productDtos, (int)query.PageIndex, result.TotalPages, (int)query.PageSize);
        }

        public async Task<ProductDto> GetProductById(GetProductByIdQuery query)
        {
            var product = await _repository.GetById(query.ProductId)
                ?? throw new NotFoundException("Cannot find product");
            var category = await _categoryRepository.GetById(product.CategoryId)
                ?? throw new NotFoundException("Cannot find product category");
            var brand = await _brandRepository.GetById(product.BrandId)
                ?? throw new NotFoundException("Cannot find product brand");
            var res = _mapper.Map<ProductDto>(product);
            res.Category = _mapper.Map<CategoryDto>(category);
            res.Brand = _mapper.Map<BrandDto>(brand);
            return res;
        }

        public async Task<bool> UpdateProduct(UpdateProductCommand command)
        {
            var product = await _repository.GetById(command.Id)
                ?? throw new NotFoundException("Cannot find this product");

            product.Status = command.Status;
            product.Name = command.Name;
            product.Description = command.Description;
            product.ShortDescription = command.ShortDescription;
            product.Unit = command.Unit;
            product.Code = command.Code;
            product.CategoryId = command.CategoryId;
            product.BrandId = command.BrandId;
            product.Quantity = command.Quantity;
            product.Slug = command.Name.Slugify();
            product.SaleId = command.SaleId;
            product.Cost = command.Cost;
            var image = "";
            //if (command.Image != null)
            //{
            //    var defaultImage = product.Images.Where(x => x.IsDefault == true).FirstOrDefault();
            //    if (defaultImage != null)
            //    {
            //        image = defaultImage?.Image;
            //        defaultImage.Image = await _uploadService.UploadFile(command.Image);
            //        UtilMethod.SetAuditable(DateTime.Now, defaultImage, isUpdate: true);
            //    }
            //}
            //if (command.Variants != null)
            //{
            //    command.Variants.ForEach(v =>
            //    {
            //        if (string.IsNullOrEmpty(v.Id))
            //        {
            //            var totalPrice = v.Quantity * v.ItemPrice;
            //            var i = product.Variants.Count();
            //            var variant = new Variant()
            //            {
            //                Id = Guid.NewGuid().ToString(),
            //                Name = v.Name,
            //                ItemCost = v.ItemCost,
            //                ItemPrice = v.ItemPrice,
            //                PromotionalItemPrice = v.ItemPrice,
            //                Quantity = v.Quantity,
            //                TotalPrice = totalPrice,
            //                Status = PRODUCT_STATUS.ACTIVE_PRODUCT,
            //                Sku = $"{product.Code}-{i}"
            //            };
            //            UtilMethod.SetAuditable(DateTime.UtcNow, variant);
            //            product.Variants.Add(variant);
            //        }
            //        else
            //        {
            //            var variant = product.Variants.Where(x => x.Id == v.Id).FirstOrDefault();
            //            if (variant != null)
            //            {
            //                variant.Name = v.Name;
            //                variant.ItemCost = v.ItemCost;
            //                variant.ItemPrice = v.ItemPrice;
            //                variant.Quantity = v.Quantity;
            //                variant.TotalPrice = v.Quantity * v.ItemPrice;
            //                variant.Status = v.Status;
            //            }
            //            UtilMethod.SetAuditable(DateTime.UtcNow, variant, isUpdate: true);
            //        }
            //    });
            //}
            var res = _repository.Update(product);

            if (res && !string.IsNullOrEmpty(image))
            {
                await _uploadService.DeleteFile(image);
            }

            return res;
        }

        public override async Task<ProductResponse> GetProductInformation(GetProductDetailByVariant request, ServerCallContext context)
        {
            var product = await _repository.GetProductByVariant(request.VariantId);
            if (product == null)
                return null;
            var variant = product.Variants.Where(x => x.Id == request.VariantId).FirstOrDefault();
            var category = await _categoryRepository.GetById(product.CategoryId)
                ?? throw new NotFoundException("Cannot find product category");
            var brand = await _brandRepository.GetById(product.BrandId)
                ?? throw new NotFoundException("Cannot find product brand");
            var productDto = _mapper.Map<ProductDto>(product);
            return new ProductResponse()
            {
                Description = productDto.Description,
                Image = productDto.Images.Where(x => x.IsDefault = true)?.FirstOrDefault()?.Image,
                Name = productDto.Name,
                CategoryId = productDto.CategoryId,
                CategoryName = category.Name,
                CategorySlug = category.Slug,
                BrandName = brand.Name,
                BrandId = brand.Id,
                ProductId = product.Id,
                PromotionalPrice = (double)productDto.PromotionalPrice,
                Rating = productDto.Rating,
                SaleId = productDto.SaleId,
                Slug = productDto.Slug,
                Sold = productDto.Sold,
                Status = productDto.Status,
                Price = (double)productDto.Price,
                Quantity = productDto.Quantity,
                VariantName = variant.Name,
                VariantQuantity = variant.Quantity,
                VariantTotalPrice = (double)(variant.TotalPrice),
                Unit = product.Unit,
                Cost = (double)product.Cost
            };
        }

        public async Task<bool> UpdateProductQuantity(UpdateProductQuantityCommand command)
        {
            return await _repository.UpdateProductQuantity(command);
        }

        public async Task<bool> UpdateProductImage(UpdateProductImageCommand command)
        {
            var product = await _repository.GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var image = "";
            if (command.Image != null)
            {
                var prdImg = product.Images.Where(x => x.Id == command.ProductImageId).FirstOrDefault();
                if (prdImg != null)
                {
                    image = prdImg.Image;
                    prdImg.Image = await _uploadService.UploadFile(command.Image);
                    prdImg.Size = command.Image.Length;
                    prdImg.ContentType = command.Image.ContentType;
                    prdImg.IsDefault = command.IsDefault;
                }
                UtilMethod.SetAuditable(DateTime.Now, prdImg, isUpdate: true);
            }
            var isSuccess = _repository.Update(product);

            if (!string.IsNullOrEmpty(image) && isSuccess)
                await _uploadService.DeleteFile(image);
            return isSuccess;
        }

        public async Task<bool> DeleteProductImage(DeleteProductImageCommand command)
        {
            var product = await _repository.GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var productImg = product.Images
                .Where(x => x.Id == command.ProductImageId)
                .FirstOrDefault()
                ?? throw new NotFoundException("Cannot find this product image");
            var isRemoveSuccess = product.Images.Remove(productImg);
            var isSuccess = _repository.Update(product) && isRemoveSuccess;
            if (isSuccess)
            {
                await _uploadService.DeleteFile(productImg.Image);
            }

            return isSuccess;
        }

        public async Task<PaginatedResult<ProductImageDto>> GetAllProductImage(GetAllProductImageQuery query)
        {
            var product = await _repository.GetById(query.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var productImageDtos = new List<ProductImageDto>();
            foreach (var image in product.Images)
            {
                productImageDtos.Add(_mapper.Map<ProductImageDto>(image));
            }
            return new PaginatedResult<ProductImageDto>(productImageDtos, (int)query.PageIndex, product.Images.Count, (int)query.PageSize);
        }

        public async Task<bool> AddProductImage(CreateProductImageCommand command)
        {
            var product = await _repository.GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find this product");

            var prdImg = new ProductImage()
            {
                Id = Guid.NewGuid().ToString(),
                Image = await _uploadService.UploadFile(command.Image),
                ContentType = command.Image.ContentType,
                Size = command.Image.Length,
                IsDefault = command.IsDefault
            };
            UtilMethod.SetAuditable(DateTime.Now, prdImg);
            product.Images.Add(prdImg);

            return _repository.Update(product);
        }
    }
}