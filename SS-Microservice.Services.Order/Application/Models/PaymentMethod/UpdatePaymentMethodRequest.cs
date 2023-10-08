namespace SS_Microservice.Services.Order.Application.Models.PaymentMethod
{
    public class UpdatePaymentMethodRequest
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string Code { get; set; }
        public bool Status { get; set; }
        public IFormFile Image { get; set; }
    }
}