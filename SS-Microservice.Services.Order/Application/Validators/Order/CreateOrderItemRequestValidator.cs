using FluentValidation;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Validators.Order
{
    public class CreateOrderItemRequestValidator : AbstractValidator<CreateOrderItemRequest>
    {
        public CreateOrderItemRequestValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().NotNull();
            RuleFor(x => x.VariantId).NotEmpty().NotNull();
        }
    }
}