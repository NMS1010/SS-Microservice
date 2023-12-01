using FluentValidation;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Validators
{
    public class CreateBasketItemRequestValidator : AbstractValidator<CreateBasketItemRequest>
    {
        public CreateBasketItemRequestValidator()
        {
            RuleFor(x => x.VariantId).NotEmpty().NotNull();
        }
    }
}