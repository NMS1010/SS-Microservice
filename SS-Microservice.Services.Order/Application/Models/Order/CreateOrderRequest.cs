using SS_Microservice.Services.Order.Application.Models.OrderItem;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class CreateOrderRequest
    {
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public int OrderStateId { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; }
    }
}