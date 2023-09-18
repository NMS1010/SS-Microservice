﻿using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Basket.Application.Interfaces.Repositories;
using SS_Microservice.Services.Basket.Domain.Entities;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;
using System.Linq.Expressions;

namespace SS_Microservice.Services.Basket.Infrastructure.Repositories
{
    public class BasketItemRepository : GenericRepository<BasketItem>, IBasketItemRepository
    {
        private readonly BasketDBContext _dbContext;

        public BasketItemRepository(BasketDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> DeleteBasketItem(List<string> productIds, int basketId)
        {
            var count = await _dbContext.BasketItems.Where(x => x.BasketId == basketId && productIds.Contains(x.ProductId)).ExecuteDeleteAsync();

            return count > 0;
        }

        public async Task<PaginatedResult<BasketItem>> GetBasketItem(int basketId, int pageIndex, int pageSize)
        {
            var res = _dbContext.BasketItems.Where(x => x.BasketId == basketId);

            return await res.PaginatedListAsync(pageIndex, pageSize);
        }

        public async Task<BasketItem> IsBasketItemExist(int basketId, string productId)
        {
            return await _dbContext.BasketItems.Where(x => x.BasketId == basketId
                && x.ProductId == productId).FirstOrDefaultAsync();
        }
    }
}