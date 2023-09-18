using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces.Repositories
{
    public interface IOrderRepository : IGenericRepository<Domain.Entities.Order>
    {
        Task<PaginatedResult<Domain.Entities.Order>> GetOrderList(GetAllOrderQuery query);

        Task<Domain.Entities.Order> GetOrder(GetOrderByIdQuery query);
    }
}