using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Interfaces.Repositories
{
    public interface IOrderStateRepository : IGenericRepository<OrderState>
    {
        Task<PaginatedResult<OrderState>> GetOrderStateList(GetAllOrderStateQuery query);
    }
}