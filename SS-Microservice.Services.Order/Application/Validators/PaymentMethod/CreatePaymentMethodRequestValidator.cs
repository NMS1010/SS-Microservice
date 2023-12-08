using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;

namespace SS_Microservice.Services.Order.Application.Validators.PaymentMethod
{
    public class CreatePaymentMethodRequestValidator : AbstractValidator<CreatePaymentMethodRequest>
    {
        public CreatePaymentMethodRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Image).NotEmpty().NotNull();
        }
    }
}