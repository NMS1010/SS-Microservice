using FluentValidation;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Validators.Review
{
    internal class UpdateReviewRequestValidator : AbstractValidator<UpdateReviewRequest>
    {
        public UpdateReviewRequestValidator()
        {
            RuleFor(x => x.Rating).NotEmpty().NotNull();
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.Content).NotEmpty().NotNull();
        }
    }
}