using FluentValidation;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Validators.Review
{
    public class ReplyReviewRequestValidator : AbstractValidator<ReplyReviewRequest>
    {
        public ReplyReviewRequestValidator()
        {
            //RuleFor(x => x.Reply).NotEmpty().NotNull();
        }
    }
}