﻿using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Basket.Domain.Entities;

namespace SS_Microservice.Services.Basket.Application.Interfaces.Repositories
{
    public interface IBasketItemRepository : IGenericRepository<BasketItem>
    {
        Task<PaginatedResult<BasketItem>> GetBasketItem(int basketId, int pageIndex, int pageSize);

        Task<BasketItem> IsBasketItemExist(int basketId, string productId);

        Task<bool> DeleteBasketItem(List<string> productIds, int basketId);
    }
}