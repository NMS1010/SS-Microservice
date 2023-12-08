using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;

namespace SS_Microservice.Services.Order.Application.Validators.OrderCancellationReason
{
    public class UpdateOrderCancellationReasonRequestValidator : AbstractValidator<UpdateOrderCancellationReasonRequest>
    {
        public UpdateOrderCancellationReasonRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Status).NotNull();
        }
    }
}