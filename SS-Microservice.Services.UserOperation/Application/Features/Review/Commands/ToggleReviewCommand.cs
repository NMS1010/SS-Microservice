using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class ToggleReviewCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class ToggleReviewHandler : IRequestHandler<ToggleReviewCommand, bool>
    {
        private readonly IReviewService _reviewService;

        public ToggleReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<bool> Handle(ToggleReviewCommand request, CancellationToken cancellationToken)
        {
            //pub event to product service to update review

            return await _reviewService.ToggleReview(request);
        }
    }
}
