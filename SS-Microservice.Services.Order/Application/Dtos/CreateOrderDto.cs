namespace SS_Microservice.Services.Order.Application.Dtos
{
    public class CreateOrderDto
    {
        public string OrderCode { get; set; }
        public long OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
