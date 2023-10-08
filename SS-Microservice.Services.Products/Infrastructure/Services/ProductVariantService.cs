using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Products.Application.Common;
using SS_Microservice.Services.Products.Application.Common.Constants;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Variant.Commands;
using SS_Microservice.Services.Products.Application.Features.Variant.Queries;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SS_Microservice.Services.Products.Infrastructure.Services
{
    public class ProductVariantService : IProductVariantService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductVariantService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> AddVariant(CreateVariantCommand command)
        {
            var product = await _repository.GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var count = product.Variants.Count;
            var variant = new Variant()
            {
                Id = Guid.NewGuid().ToString(),
                ItemPrice = command.ItemPrice,
                Quantity = command.Quantity,
                Name = command.Name,
                Sku = $"{product.Code}-{count}",
                Status = PRODUCT_STATUS.ACTIVE_PRODUCT,
                TotalPrice = command.ItemPrice * command.Quantity,
                PromotionalItemPrice = command.ItemPrice
            };
            UtilMethod.SetAuditable(DateTime.UtcNow, variant);
            product.Variants.Add(variant);
            return _repository.Update(product);
        }

        public async Task<bool> DeleteVariant(DeleteVariantCommand command)
        {
            var product = await _repository.GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var variant = product.Variants.Where(x => x.Id == command.VariantId).FirstOrDefault()
                ?? throw new NotFoundException("Cannot find this variant of product");
            variant.Status = PRODUCT_STATUS.INACTIVE_PRODUCT;

            return _repository.Update(product);
        }

        public async Task<PaginatedResult<VariantDto>> GetAllVariant(GetAllVariantQuery query)
        {
            var product = await _repository.GetById(query.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var variantDtos = new List<VariantDto>();
            foreach (var item in product.Variants)
            {
                variantDtos.Add(_mapper.Map<VariantDto>(item));
            }

            return new PaginatedResult<VariantDto>(variantDtos, (int)query.PageIndex, product.Variants.Count, (int)query.PageSize);
        }

        public async Task<VariantDto> GetVariantById(GetVariantByIdQuery query)
        {
            var product = await _repository.GetById(query.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var variant = product.Variants.Where(x => x.Id == query.VariantId).FirstOrDefault()
                ?? throw new NotFoundException("Cannot find this variant of product");
            return _mapper.Map<VariantDto>(variant);
        }

        public async Task<bool> UpdateVariant(UpdateVariantCommand command)
        {
            var product = await _repository.GetById(command.ProductId)
                ?? throw new NotFoundException("Cannot find this product");
            var variant = product.Variants.Where(x => x.Id == command.VariantId).FirstOrDefault()
                ?? throw new NotFoundException("Cannot find this variant of product");
            variant.ItemPrice = command.ItemPrice;
            variant.Quantity = command.Quantity;
            variant.Name = command.Name;
            variant.Status = command.Status;
            variant.TotalPrice = command.ItemPrice * command.Quantity;
            UtilMethod.SetAuditable(DateTime.UtcNow, variant, isUpdate: true);

            return _repository.Update(product);
        }
    }
}