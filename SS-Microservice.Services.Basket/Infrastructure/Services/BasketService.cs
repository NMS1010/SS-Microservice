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
using System.ComponentModel.DataAnnotations;

namespace SS_Microservice.Services.Basket.Infrastructure.Services
{
	public class BasketService : IBasketService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public BasketService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<bool> AddProductToBasket(AddBasketCommand command)
		{
			//var basket = (await _basketRepository.GetAll()).Where(x => x.UserId == command.UserId).FirstOrDefault();
			var basketSpec = new BasketSpecification(command.UserId);
			var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(basketSpec);
			var basketId = basket?.Id;
			if (basket == null)
			{
				basketId = await CreateBasket(new CreateBasketCommand() { UserId = command.UserId });
				if (basketId <= 0)
					throw new NotFoundException("Cannot find user's basket");
			}
			var basketItem = new BasketItem()
			{
				BasketId = basketId.Value,
				VariantId = command.VariantId,
				Quantity = command.Quantity,
				IsSelected = 0,
			};
			//var b = await _basketItemRepository.IsBasketItemExist(basketId.Value, command.ProductId);
			var basketItemSpec = new BasketItemSpecification(basketId.Value, command.VariantId);
			var b = await _unitOfWork.Repository<BasketItem>().GetEntityWithSpec(basketItemSpec);
			try
			{
				await _unitOfWork.CreateTransaction();
				// if exist, increase quantity
				if (b != null)
				{
					b.Quantity += command.Quantity;
					_unitOfWork.Repository<BasketItem>().Update(b);
				}
				// otherwise, insert new item
				else
				{
					await _unitOfWork.Repository<BasketItem>().Insert(basketItem);
				}
				var isSuccess = await _unitOfWork.Save() > 0;
				if (!isSuccess)
					throw new Exception("Cannot add product to your basket");
				await _unitOfWork.Commit();
				return isSuccess;
			}
			catch
			{
				await _unitOfWork.Rollback();
				throw;
			}
		}

		public async Task<BasketDto> GetBasket(GetBasketQuery query)
		{
			//var basket = (await _basketRepository.GetAll()).Where(x => x.UserId == query.UserId).FirstOrDefault();
			var basketSpec = new BasketSpecification(query.UserId);
			var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(basketSpec);
			if (basket == null)
			{
				var basketId = await CreateBasket(new CreateBasketCommand() { UserId = query.UserId });
				basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetById(basketId)
					?? throw new NotFoundException("Cannot find user's basket");
			}
			var basketItemSpec = new BasketItemSpecification(query, isPaging: true);
			var basketItemCountSpec = new BasketItemSpecification(query);
			var basketItems = await _unitOfWork.Repository<BasketItem>().ListAsync(basketItemSpec);
			var totalCount = await _unitOfWork.Repository<BasketItem>().CountAsync(basketItemCountSpec);

			var basketItemDto = new List<BasketItemDto>();
			foreach (var item in basketItems)
			{
				basketItemDto.Add(_mapper.Map<BasketItemDto>(item));
			}

			return new BasketDto()
			{
				BasketItems = new PaginatedResult<BasketItemDto>(basketItemDto, (int)query.PageIndex, totalCount, (int)query.PageSize)
			};
		}

		private async Task<BasketItem> FindBasketItem(string userId, long basketItemId)
		{
			var basketItem = await _unitOfWork.Repository<BasketItem>().GetById(basketItemId)
				?? throw new NotFoundException("Cannot find this basket item ");
			var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetById(basketItem.BasketId)
				?? throw new NotFoundException("Cannot regconize user's basket");
			if (basket.UserId != userId)
				throw new ValidationException("Cannot regconize user's basket");

			return basketItem;
		}

		public async Task<bool> RemoveProductFromBasket(DeleteBasketCommand command)
		{
			try
			{
				await _unitOfWork.CreateTransaction();
				var basketItem = await FindBasketItem(command.UserId, command.BasketItemId);
				_unitOfWork.Repository<BasketItem>().Delete(basketItem);
				var isSuccess = await _unitOfWork.Save() > 0;
				if (!isSuccess)
					throw new Exception("Cannot remove product from this basket");

				await _unitOfWork.Commit();
				return isSuccess;
			}
			catch
			{
				await _unitOfWork.Rollback();
				throw;
			}
		}

		public async Task<bool> UpdateProductQuantity(UpdateBasketCommand command)
		{
			try
			{
				await _unitOfWork.CreateTransaction();
				var basketItem = await FindBasketItem(command.UserId, command.BasketItemId);

				basketItem.Quantity = command.Quantity;

				_unitOfWork.Repository<BasketItem>().Update(basketItem);
				var isSuccess = await _unitOfWork.Save() > 0;
				if (!isSuccess)
					throw new Exception("Cannot update product Quantity");
				await _unitOfWork.Commit();

				return isSuccess;
			}
			catch
			{
				await _unitOfWork.Rollback();
				throw;
			}
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

		public async Task<bool> ClearBasket(ClearBasketCommand command)
		{
			try
			{
				var basketSpec = new BasketSpecification(command.UserId);
				var basket = await _unitOfWork.Repository<Domain.Entities.Basket>().GetEntityWithSpec(basketSpec);
				if (basket == null)
					return false;

				var basketItemSpec = new BasketItemSpecification(command.VariantIds, basket.Id);
				var basketItems = await _unitOfWork.Repository<BasketItem>().ListAsync(basketItemSpec);

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
	}
}