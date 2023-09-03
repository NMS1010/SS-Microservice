using AutoMapper;
using Org.BouncyCastle.Bcpg;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Message.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Message.Basket.Queries;
using SS_Microservice.Services.Basket.Core.Entities;
using SS_Microservice.Services.Basket.Core.Interfaces;
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

        public async Task<bool> AddProductToBasket(AddBasketCommand command)
        {
            var now = DateTime.Now;
            var basket = (await _basketRepository.GetAll()).Where(x => x.UserId == command.UserId).FirstOrDefault();
            var basketId = basket?.BasketId;
            if (basket == null)
            {
                basketId = await CreateBasket(new CreateBasketCommand() { UserId = command.UserId });
                if (basketId <= 0)
                    throw new NotFoundException("Cannot find user's basket");
            }
            var basketItem = new BasketItem()
            {
                BasketId = basketId.Value,
                ProductId = command.ProductId,
                Quantity = command.Quantity,
                IsSelected = 0,
                DateCreated = now,
                DateUpdated = now,
            };
            var b = await _basketItemRepository.IsBasketItemExist(basketId.Value, command.ProductId);
            // if exist, increase quantity
            if (b != null)
            {
                b.Quantity += command.Quantity;
                return _basketItemRepository.Update(b);
            }
            // otherwise, insert new item
            else
            {
                return await _basketItemRepository.Insert(basketItem);
            }
        }

        public async Task<BasketDto> GetBasket(GetBasketQuery query)
        {
            var basket = (await _basketRepository.GetAll()).Where(x => x.UserId == query.UserId).FirstOrDefault();
            if (basket == null)
            {
                var basketId = await CreateBasket(new CreateBasketCommand() { UserId = query.UserId });
                basket = await _basketRepository.GetById(basketId) ?? throw new NotFoundException("Cannot find user's basket");
            }

            var basketItems = await _basketItemRepository.GetBasketItem(basket.BasketId, (int)query.PageIndex, (int)query.PageSize);
            var basketItemDto = new List<BasketItemDto>();
            foreach (var item in basketItems.Items)
            {
                basketItemDto.Add(_mapper.Map<BasketItemDto>(item));
            }
            var basketDto = new BasketDto()
            {
                BasketItems = new PaginatedResult<BasketItemDto>(basketItemDto, (int)query.PageIndex, basketItems.TotalCount, (int)query.PageSize)
            };
            return basketDto;
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

        public async Task<bool> RemoveProductFromBasket(DeleteBasketCommand command)
        {
            var basketItem = await FindBasketItem(command.UserId, command.BasketItemId);
            return _basketItemRepository.Delete(basketItem);
        }

        public async Task<bool> UpdateProductQuantity(UpdateBasketCommand command)
        {
            var basketItem = await FindBasketItem(command.UserId, command.BasketItemId);

            basketItem.Quantity = command.Quantity;

            return _basketItemRepository.Update(basketItem);
        }

        public async Task<int> CreateBasket(CreateBasketCommand command)
        {
            var basket = new Core.Entities.Basket()
            {
                UserId = command.UserId
            };
            await _basketRepository.Insert(basket);
            return basket.BasketId;
        }

        public async Task<bool> ClearBasket(ClearBasketCommand command)
        {
            return await _basketItemRepository.DeleteBasketItem(command.BasketId);
        }
    }
}