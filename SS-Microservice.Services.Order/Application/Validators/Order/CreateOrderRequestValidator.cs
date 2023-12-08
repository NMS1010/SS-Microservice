using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Validators.Order
{
    public class CreateOrderRequestValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderRequestValidator()
        {
            RuleFor(x => x.PaymentMethodId).NotEmpty().NotNull();
            RuleFor(x => x.DeliveryId).NotEmpty().NotNull();
            RuleFor(x => x.Items).NotNull();
        }
    }
}