using AutoMapper;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Basket.Application.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace SS_Microservice.Services.Basket.Infrastructure.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IMapper _mapper;

        public BasketService(IBasketRepository basketRepository, IBasketItemRepository basketItemRepository, IMapper mapper)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _mapper = mapper;
        }

        public async Task AddProductToBasket(BasketAddCommand command)
        {
            var now = DateTime.Now;
            var basket = (await _basketRepository.GetAll()).Where(x => x.UserId == command.UserId).FirstOrDefault();
            var basketItem = new BasketItem()
            {
                ProductId = command.ProductId,
                Quantity = command.Quantity,
                IsSelected = 0,
                DateCreated = now,
                DateUpdated = now,
            };
            if (basket != null)
            {
                var b = basket.BasketItems?.Where(x => x.ProductId == command.ProductId).FirstOrDefault();
                // if exist, increase quantity
                if (b != null)
                {
                    b.Quantity += 1;
                    _basketItemRepository.Update(b);
                }
                // otherwise, insert new item
                else
                {
                    basketItem.BasketId = basket.BasketId;
                    await _basketItemRepository.Insert(basketItem);
                }
            }
            else
            {
                basket = new Core.Entities.Basket()
                {
                    UserId = command.UserId,
                    BasketItems = new List<BasketItem>()
                    {
                        {
                            basketItem
                        }
                    }
                };
                await _basketRepository.Insert(basket);
            }
        }

        public async Task<BasketDto> GetBasket(BasketGetQuery query)
        {
            var basket = (await _basketRepository.GetAll()).Where(x => x.UserId == query.UserId).FirstOrDefault()
                ?? throw new NotFoundException("Cannot found basket of this user");
            return _mapper.Map<BasketDto>(basket);
        }

        private async Task<BasketItem> FindBasketItem(string userId, int basketItemId)
        {
            var basketItem = await _basketItemRepository.GetById(basketItemId)
                ?? throw new NotFoundException("Cannot found this basket item ");
            var basket = await _basketRepository.GetById(basketItem.BasketId)
                ?? throw new NotFoundException("Cannot regconize user's basket");
            if (basket.UserId != userId)
                throw new ValidationException("Cannot regconize user's basket");

            return basketItem;
        }

        public async Task<bool> RemoveProductFromBasket(BasketDeleteCommand command)
        {
            var basketItem = await FindBasketItem(command.UserId, command.BasketItemId);
            return _basketItemRepository.Delete(basketItem);
        }

        public async Task<bool> UpdateProductQuantity(BasketUpdateCommand command)
        {
            var basketItem = await FindBasketItem(command.UserId, command.BasketItemId);

            basketItem.Quantity = command.Quantity;

            return _basketItemRepository.Update(basketItem);
        }
    }
}