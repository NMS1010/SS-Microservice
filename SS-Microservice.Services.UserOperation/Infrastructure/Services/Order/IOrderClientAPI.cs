using RestEase;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Order.Model.Response;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Order
{
    public interface IOrderClientAPI
    {
        [Get("api/orders/order-items/{id}")]
        Task<OrderItemDto> GetOrderItem([Path] long id);
    }
}
