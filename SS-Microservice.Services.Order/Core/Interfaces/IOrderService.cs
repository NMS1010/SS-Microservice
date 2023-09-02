using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Message.Order.Commands;
using SS_Microservice.Services.Order.Application.Message.Order.Queries;

namespace SS_Microservice.Services.Order.Core.Interfaces
{
    public interface IOrderService
    {
        Task<PaginatedResult<OrderDto>> GetOrderList(GetAllOrderQuery query);

        Task<OrderDto> GetOrder(GetOrderByIdQuery query);

        Task CreateOrder(CreateOrderCommand command);

        Task<bool> UpdateOrder(UpdateOrderCommand command);

        Task<bool> DeleteOrder(DeleteOrderCommand command);
    }
}