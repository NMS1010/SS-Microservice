using FluentValidation;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Application.Validators
{
    public class UpdateBasketItemRequestValidator : AbstractValidator<UpdateBasketItemRequest>
    {
        public UpdateBasketItemRequestValidator()
        {
            RuleFor(x => x.Quantity).NotEmpty().NotNull();
        }
    }
}