using FluentValidation;
using SS_Microservice.Services.Order.Application.Common.Constants;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Validators.Order
{
    public class UpdateOrderRequestValidator : AbstractValidator<UpdateOrderRequest>
    {
        public UpdateOrderRequestValidator()
        {
            RuleFor(x => x.Status).NotEmpty().NotNull();
            RuleFor(x => x.Status)
                .Must(x => ORDER_STATUS.Status.Contains(x))
                .WithMessage("Unexpected order status");
        }
    }
}