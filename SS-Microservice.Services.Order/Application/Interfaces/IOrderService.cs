using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Infrastructure.Consumers.Events.OrderingSaga;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IOrderService
    {
        Task<CreateOrderDto> CreateOrder(CreateOrderCommand command);

        Task<bool> CompletePaypalOrder(CompletePaypalOrderCommand command);

        Task<bool> UpdateOrder(UpdateOrderCommand command);

        Task<bool> DeleteOrder(DeleteOrderCommand command);

        Task<OrderDto> GetOrder(GetOrderQuery query);

        Task<OrderDto> GetOrderByCode(GetOrderByCodeQuery query);

        Task<PaginatedResult<OrderDto>> GetListOrder(GetListOrderQuery query);

        Task<List<OrderDto>> GetTopOrderLatest(GetTopLatestOrderQuery query);

        Task<PaginatedResult<OrderDto>> GetListUserOrder(GetListUserOrderQuery query);
    }
}