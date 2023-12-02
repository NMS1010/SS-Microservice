using FluentValidation;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;

namespace SS_Microservice.Services.UserOperation.Application.Validators.UserFollowProduct
{
    public class FollowProductRequestValidator : AbstractValidator<FollowProductRequest>
    {
        public FollowProductRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
        }
    }
}