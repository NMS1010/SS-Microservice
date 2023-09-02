using SS_Microservice.Services.Order.Application.Models.OrderItem;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class OrderCreateRequest
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public List<OrderItemCreateRequest> Items { get; set; }
    }
}