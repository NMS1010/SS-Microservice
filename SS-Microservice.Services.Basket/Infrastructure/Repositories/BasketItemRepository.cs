using MongoDB.Driver;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Basket.Application.Common.Interfaces;
using SS_Microservice.Services.Basket.Core.Entities;
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

        public async Task<PaginatedResult<BasketItem>> GetBasketItem(int basketId, int pageIndex, int pageSize)
        {
            var res = _dbContext.BasketItems.Where(x => x.BasketId == basketId);

            return await res.PaginatedListAsync(pageIndex, pageSize);
        }
    }
}