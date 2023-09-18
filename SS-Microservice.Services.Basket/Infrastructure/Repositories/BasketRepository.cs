using SS_Microservice.Services.Basket.Application.Interfaces.Repositories;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;

namespace SS_Microservice.Services.Basket.Infrastructure.Repositories
{
    public class BasketRepository : GenericRepository<Domain.Entities.Basket>, IBasketRepository
    {
        public BasketRepository(BasketDBContext dbContext) : base(dbContext)
        {
        }
    }
}