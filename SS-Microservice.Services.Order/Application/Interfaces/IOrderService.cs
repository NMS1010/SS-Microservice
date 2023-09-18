using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;

namespace SS_Microservice.Services.Order.Application.Interfaces
{
    public interface IOrderService
    {
        Task<PaginatedResult<OrderDto>> GetOrderList(GetAllOrderQuery query);

        Task<OrderDto> GetOrder(GetOrderByIdQuery query);

        Task<(bool, long)> CreateOrder(CreateOrderCommand command);

        Task<bool> UpdateOrder(UpdateOrderCommand command);

        Task<bool> DeleteOrder(DeleteOrderCommand command);
    }
}