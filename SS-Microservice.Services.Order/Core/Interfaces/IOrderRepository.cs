using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Message.Order.Queries;

namespace SS_Microservice.Services.Order.Core.Interfaces
{
    public interface IOrderRepository : IGenericRepository<Core.Entities.Order>
    {
        Task<PaginatedResult<Core.Entities.Order>> GetOrderList(GetAllOrderQuery query);

        Task<Core.Entities.Order> GetOrder(GetOrderByIdQuery query);
    }
}