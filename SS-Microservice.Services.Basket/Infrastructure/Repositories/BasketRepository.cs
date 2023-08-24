using SS_Microservice.Services.Basket.Application.Common.Interfaces;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;

namespace SS_Microservice.Services.Basket.Infrastructure.Repositories
{
    public class BasketRepository : GenericRepository<Core.Entities.Basket>, IBasketRepository
    {
        public BasketRepository(BasketDBContext dbContext) : base(dbContext)
        {
        }
    }
}