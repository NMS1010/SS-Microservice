using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Message.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Message.OrderState.Queries;

namespace SS_Microservice.Services.Order.Core.Interfaces
{
    public interface IOrderStateService
    {
        Task<PaginatedResult<OrderStateDto>> GetOrderStateList(GetAllOrderStateQuery query);

        Task<OrderStateDto> GetOrderState(GetOrderStateByIdQuery query);

        Task CreateOrderState(CreateOrderStateCommand command);

        Task<bool> UpdateOrderState(UpdateOrderStateCommand command);

        Task<bool> DeleteOrderState(DeleteOrderStateCommand command);
    }
}