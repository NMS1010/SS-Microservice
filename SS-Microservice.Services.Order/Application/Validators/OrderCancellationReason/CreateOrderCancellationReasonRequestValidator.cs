using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;

namespace SS_Microservice.Services.Order.Application.Validators.OrderCancellationReason
{
    public class CreateOrderCancellationReasonRequestValidator : AbstractValidator<CreateOrderCancellationReasonRequest>
    {
        public CreateOrderCancellationReasonRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
        }
    }
}