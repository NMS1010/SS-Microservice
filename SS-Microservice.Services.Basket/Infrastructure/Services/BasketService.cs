using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Specifications;
using SS_Microservice.Services.Basket.Domain.Entities;

namespace SS_Microservice.Services.Basket.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BasketService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<long> CreateBasket(CreateBasketCommand command)
        {
            try
            {
                await _unitOfWork.CreateTransaction();
                var basket = new Domain.Entities.Basket()
                {
                    UserId = command.UserId
                };
                await _unitOfWork.Repository<Domain.Entities.Basket>().Insert(basket);
                await _unitOfWork.Save();
                await _unitOfWork.Commit();
                return basket.Id;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> CreateBasketItem(CreateBasketItemCommand command)
        {
            var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(new BasketSpecification(command.UserId))
                ?? throw new NotFoundException("Cannot find basket of current user");

            var basketItem = await _unitOfWork.Repository<BasketItem>().GetEntityWithSpec(new BasketItemSpecification(basket.Id, command.VariantId));

            var ci = new BasketItem();

            //var variant = await _unitOfWork.Repository<Variant>().GetEntityWithSpec(new VariantSpecification(command.VariantId))
            //    ?? throw new InvalidRequestException("Unexpected variantId");

            //if (variant.Product.Status != PRODUCT_STATUS.ACTIVE)
            //    throw new InvalidRequestException("Unexpected variantId, product is not active");

            //var quantity = variant?.Product?.Quantity;
            //if (quantity < command.Quantity)
            //    throw new InvalidRequestException("Unexpected quantity, it must be less than or equal to product in inventory");

            if (basketItem == null)
            {
                ci.Quantity = command.Quantity;
                ci.VariantId = command.VariantId;
                basket.BasketItems.Add(ci);
                _unitOfWork.Repository<Domain.Entities.Basket>().Update(basket);
            }
            else
            {
                basketItem.Quantity += command.Quantity;
                _unitOfWork.Repository<BasketItem>().Update(basketItem);
            }

            var isSuccess = await _unitOfWork.Save() > 0;

            if (!isSuccess)
                throw new Exception("Cannot handle to add product to your basket, an error has occured");

            return isSuccess;
        }

        public async Task<bool> DeleteBasketItem(DeleteBasketItemCommand command)
        {
            var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(new BasketSpecification(command.UserId))
                ?? throw new NotFoundException("Cannot find basket of current user");
            var basketItem = basket.BasketItems.FirstOrDefault(x => x.Id == command.CartItemId)
                ?? throw new InvalidRequestException("Unexpected basketItemId");

            _unitOfWork.Repository<BasketItem>().Delete(basketItem);

            var isSuccess = await _unitOfWork.Save() > 0;

            if (!isSuccess)
                throw new Exception("Cannot handle to remove product from your basket, an error has occured");

            return true;
        }

        public async Task<bool> ClearBasket(ClearBasketCommand command)
        {
            var basketSpec = new BasketSpecification(command.UserId);
            var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(basketSpec)
                ?? throw new NotFoundException("Cannot find basket of current user");

            var basketItemSpec = new BasketItemSpecification(command.VariantIds, basket.Id);
            var basketItems = await _unitOfWork.Repository<BasketItem>().ListAsync(basketItemSpec);

            try
            {

                await _unitOfWork.CreateTransaction();

                basketItems.ForEach(x =>
                {
                    _unitOfWork.Repository<BasketItem>().Delete(x);
                });

                var isSuccess = await _unitOfWork.Save() > 0;

                if (!isSuccess)
                {
                    throw new Exception("Cannot clear basket");
                }

                await _unitOfWork.Commit();

                return isSuccess;
            }
            catch
            {
                await _unitOfWork.Rollback();
                throw;
            }
        }


        public async Task<bool> UpdateBasketItem(UpdateBasketItemCommand command)
        {
            if (command.Quantity <= 0)
                throw new InvalidRequestException("Unexpected quantity, it must be a positive number");
            var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(new BasketSpecification(command.UserId))
                ?? throw new NotFoundException("Cannot find basket of current user");

            var basketItem = await _unitOfWork.Repository<BasketItem>().GetEntityWithSpec(new BasketItemSpecification(command.CartItemId, command.UserId))
                ?? throw new InvalidRequestException("Unexpected basketItemId");

            //var quantity = basketItem?.Variant?.Product?.Quantity;
            //if (quantity < command.Quantity)
            //    throw new InvalidRequestException("Unexpected quantity, it must be less than or equal to product in inventory");

            basketItem.Quantity = command.Quantity;

            _unitOfWork.Repository<Domain.Entities.Basket>().Update(basket);

            var isSuccess = await _unitOfWork.Save() > 0;

            if (!isSuccess) throw new Exception("Cannot handle to update product quantity, an error has occured");

            return isSuccess;
        }

        public async Task<bool> DeleteListBasketItem(DeleteListBasketItemCommand command)
        {
            var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(new BasketSpecification(command.UserId))
                ?? throw new NotFoundException("Cannot find basket of current user");

            foreach (var id in command.Ids)
            {
                var basketItem = basket.BasketItems.FirstOrDefault(x => x.Id == id)
                    ?? throw new InvalidRequestException("Unexpected basketItemId");

                _unitOfWork.Repository<BasketItem>().Delete(basketItem);
            }

            var isSuccess = await _unitOfWork.Save() > 0;

            if (!isSuccess) throw new Exception("Cannot handle to remove list of product from your basket, an error has occured");

            return isSuccess;
        }

        public async Task<PaginatedResult<BasketItemDto>> GetBasketByUser(GetListBasketByUserQuery query)
        {
            var basketItems = await _unitOfWork.Repository<BasketItem>().ListAsync(new BasketItemSpecification(query, isPaging: true));
            var count = await _unitOfWork.Repository<BasketItem>().CountAsync(new BasketItemSpecification(query));

            var basketDtos = new List<BasketItemDto>();
            foreach (var basketItem in basketItems)
            {
                basketDtos.Add(await GetBasketItemDto(basketItem));
            }

            return new PaginatedResult<BasketItemDto>(basketDtos, query.PageIndex, count, query.PageSize);
        }

        private async Task<BasketItemDto> GetBasketItemDto(BasketItem basketItem)
        {
            //var isPromotion = basketItem.Variant.PromotionalItemPrice.HasValue;
            //var variant = await _unitOfWork.Repository<Variant>().GetEntityWithSpec(new VariantSpecification(basketItem.Variant.Id))
            //    ?? throw new NotFoundException("Cannot find varaint item");

            //var product = await _unitOfWork.Repository<Product>().GetEntityWithSpec(new ProductSpecification(variant.Product.Id))
            //    ?? throw new NotFoundException("Cannot find product of variant item");

            //var basketItemDto = _mapper.Map<BasketItemDto>(basketItem);

            //basketItemDto.VariantId = variant.Id;
            //basketItemDto.Quantity = basketItem.Quantity;
            //basketItemDto.TotalPrice = variant.Quantity * variant.ItemPrice;
            //basketItemDto.TotalPromotionalPrice = isPromotion ? variant.Quantity * variant.PromotionalItemPrice.Value : null;
            //basketItemDto.Sku = variant.Sku;
            //basketItemDto.VariantName = variant.Name;
            //basketItemDto.VariantPrice = variant.ItemPrice;
            //basketItemDto.VariantPromotionalPrice = isPromotion ? variant.PromotionalItemPrice.Value : null;
            //basketItemDto.IsPromotion = isPromotion;
            //basketItemDto.VariantQuantity = variant.Quantity;
            //basketItemDto.ProductName = product.Name;
            //basketItemDto.ProductSlug = product.Slug;
            //basketItemDto.ProductUnit = product.Unit.Name;
            //basketItemDto.ProductImage = product.Images.FirstOrDefault(x => x.IsDefault)?.Image ?? product.Images.FirstOrDefault()?.Image;

            //return basketItemDto;

            return new BasketItemDto();
        }


        public async Task<List<BasketItemDto>> GetBasketItemByIds(GetListBasketItemQuery query)
        {
            var res = new List<BasketItemDto>();
            foreach (var id in query.Ids)
            {
                var basketItem = await _unitOfWork.Repository<BasketItem>().GetEntityWithSpec(new BasketItemSpecification(id, query.UserId))
                    ?? throw new NotFoundException("Cannot find basket item");

                res.Add(await GetBasketItemDto(basketItem));
            }

            return res;
        }
    }
}