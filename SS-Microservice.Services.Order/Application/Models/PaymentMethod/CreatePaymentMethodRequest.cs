namespace SS_Microservice.Services.Order.Application.Models.PaymentMethod
{
    public class CreatePaymentMethodRequest
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public IFormFile Image { get; set; }
    }
}