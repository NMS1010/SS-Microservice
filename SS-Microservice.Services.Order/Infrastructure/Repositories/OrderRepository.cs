using Microsoft.EntityFrameworkCore;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces.Repositories;
using SS_Microservice.Services.Order.Infrastructure.Data.DBContext;

namespace SS_Microservice.Services.Order.Infrastructure.Repositories
{
    public class OrderRepository : GenericRepository<Domain.Entities.Order>, IOrderRepository
    {
        private readonly OrderDbContext _dbContext;

        public OrderRepository(OrderDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Domain.Entities.Order> GetOrder(GetOrderByIdQuery query)
        {
            var order = await _dbContext.Orders
                .Where(x => x.Id == query.OrderId
                && x.UserId == query.UserId)
                .Include(x => x.OrderItems)
                .FirstOrDefaultAsync() ?? throw new NotFoundException("Cannot find this order");

            return order;
        }

        public async Task<PaginatedResult<Domain.Entities.Order>> GetOrderList(GetAllOrderQuery query)
        {
            var orders = _dbContext.Orders
                .Include(x => x.OrderState)
                .AsQueryable();
            if (!string.IsNullOrEmpty(query.UserId))
            {
                orders = _dbContext.Orders.Where(x => x.UserId == query.UserId);
            }
            if (query.Keyword != null && !string.IsNullOrEmpty(query.Keyword.ToString()))
            {
                var key = query.Keyword.ToString().ToLower();
                orders = orders.Include(x => x.OrderItems).Where(x =>
                    x.OrderItems.Any(oi => oi.ProductName.ToLower().Contains(key)) ||
                    x.Id.ToString().Contains(key));
            }
            return await orders.PaginatedListAsync((int)query.PageIndex, (int)query.PageSize);
        }
    }
}