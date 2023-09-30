using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IOrderCancellationReasonService
    {
        Task<PaginatedResult<OrderCancellationReasonDto>> GetOrderCancellationReasonList(GetAllOrderCancellationReasonQuery query);

        Task<OrderCancellationReasonDto> GetOrderCancellationReason(GetOrderCancellationReasonByIdQuery query);

        Task<bool> CreateOrderCancellationReason(CreateOrderCancellationReasonCommand command);

        Task<bool> UpdateOrderCancellationReason(UpdateOrderCancellationReasonCommand command);

        Task<bool> DeleteOrderCancellationReason(DeleteOrderCancellationReasonCommand command);
    }
}