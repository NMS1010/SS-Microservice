using SS_Microservice.Services.Basket.Application.Common.Interfaces;
using SS_Microservice.Services.Basket.Core.Entities;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;

namespace SS_Microservice.Services.Basket.Infrastructure.Repositories
{
    public class BasketItemRepository : GenericRepository<BasketItem>, IBasketItemRepository
    {
        public BasketItemRepository(BasketDBContext dbContext) : base(dbContext)
        {
        }
    }
}