using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;

namespace SS_Microservice.Services.Order.Application.Validators.PaymentMethod
{
    public class UpdatePaymentMethodRequestValidator : AbstractValidator<UpdatePaymentMethodRequest>
    {
        public UpdatePaymentMethodRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Code).NotEmpty().NotNull();
            RuleFor(x => x.Status).NotNull();
        }
    }
}