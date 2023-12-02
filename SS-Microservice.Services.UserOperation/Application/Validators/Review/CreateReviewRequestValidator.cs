using FluentValidation;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Validators.Review
{
    public class CreateReviewRequestValidator : AbstractValidator<CreateReviewRequest>
    {
        public CreateReviewRequestValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().NotNull();
            RuleFor(x => x.OrderItemId).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.Rating).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
        }
    }
}