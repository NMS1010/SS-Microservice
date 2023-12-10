using AutoMapper;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Specifications;
using SS_Microservice.Services.Basket.Domain.Entities;

namespace SS_Microservice.Services.Basket.Application.Services
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
            if (command.Quantity <= 0)
                throw new InvalidRequestException("Unexpected quantity, it must be a positive number");

            var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(new BasketSpecification(command.UserId))
                ?? throw new NotFoundException("Cannot find basket of current user");

            var basketItem = await _unitOfWork.Repository<BasketItem>().GetEntityWithSpec(new BasketItemSpecification(basket.Id, command.VariantId));

            var ci = new BasketItem();


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
                basketDtos.Add(_mapper.Map<BasketItemDto>(basketItem));
            }

            return new PaginatedResult<BasketItemDto>(basketDtos, query.PageIndex, count, query.PageSize);
        }

        public async Task<List<BasketItemDto>> GetBasketItemByIds(GetListBasketItemQuery query)
        {
            var res = new List<BasketItemDto>();
            foreach (var id in query.Ids)
            {
                var basketItem = await _unitOfWork.Repository<BasketItem>().GetEntityWithSpec(new BasketItemSpecification(id, query.UserId))
                    ?? throw new NotFoundException("Cannot find basket item");

                res.Add(_mapper.Map<BasketItemDto>(basketItem));
            }

            return res;
        }
    }
}