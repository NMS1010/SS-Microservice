using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Order.Application.Message.Order.Queries;
using SS_Microservice.Services.Order.Application.Message.OrderState.Queries;

namespace SS_Microservice.Services.Order.Core.Interfaces
{
    public interface IOrderStateRepository : IGenericRepository<Core.Entities.OrderState>
    {
        Task<PaginatedResult<Core.Entities.OrderState>> GetOrderStateList(GetAllOrderStateQuery query);
    }
}