using RestEase;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Order.Model.Response;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Services.Order
{
    public interface IOrderClientAPI
    {
        [Get("api/orders/internal/order-items/{id}")]
        Task<CustomAPIResponse<OrderItemDto>> GetOrderItem([Path] long id);
    }
}
