using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IOrderCancellationReasonService
    {
        Task<long> CreateOrderCancellationReason(CreateOrderCancellationReasonCommand command);

        Task<bool> UpdateOrderCancellationReason(UpdateOrderCancellationReasonCommand command);

        Task<bool> DeleteOrderCancellationReason(DeleteOrderCancellationReasonCommand command);

        Task<bool> DeleteListOrderCancellationReason(DeleteListOrderCancellationReasonCommand command);

        Task<PaginatedResult<OrderCancellationReasonDto>> GetListOrderCancellationReason(GetListOrderCancellationReasonQuery query);

        Task<OrderCancellationReasonDto> GetOrderCancellationReason(GetOrderCancellationReasonQuery query);
    }
}