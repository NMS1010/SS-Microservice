using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Message.OrderState.Queries;
using SS_Microservice.Services.Order.Core.Entities;
using SS_Microservice.Services.Order.Core.Interfaces;
using SS_Microservice.Services.Order.Infrastructure.Data.DBContext;

namespace SS_Microservice.Services.Order.Infrastructure.Repositories
{
    public class OrderStateRepository : GenericRepository<Core.Entities.OrderState>, IOrderStateRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderStateRepository(OrderDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<OrderState>> GetOrderStateList(GetAllOrderStateQuery query)
        {
            var orderStates = _dbContext.OrderStates
                .AsQueryable();
            if (query.Keyword != null && !string.IsNullOrEmpty(query.Keyword.ToString()))
            {
                var key = query.Keyword.ToString().ToLower();
                orderStates = orderStates.Where(x =>
                    x.OrderStateName.ToLower().Contains(key) ||
                    x.OrderStateId.ToString().Contains(key) ||
                    x.Order.ToString().Contains(key) ||
                    x.HexColor.ToLower().Contains(key));
            }
            return await orderStates.PaginatedListAsync((int)query.PageIndex, (int)query.PageSize);
        }
    }
}