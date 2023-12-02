using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class ReplyReviewCommand : ReplyReviewRequest, IRequest<bool>
    {
    }

    public class ReplyReviewHandler : IRequestHandler<ReplyReviewCommand, bool>
    {
        private readonly IReviewService _reviewService;

        public ReplyReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<bool> Handle(ReplyReviewCommand request, CancellationToken cancellationToken)
        {
            return await _reviewService.ReplyReview(request);
        }
    }
}
