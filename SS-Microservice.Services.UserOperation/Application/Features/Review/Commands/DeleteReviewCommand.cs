using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class DeleteReviewCommand : IRequest<bool>
    {
        public long Id { get; set; }
    }

    public class DeleteReviewHandler : IRequestHandler<DeleteReviewCommand, bool>
    {
        private readonly IReviewService _reviewService;

        public DeleteReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            //pub event to product service to update review

            return await _reviewService.DeleteReview(request);
        }
    }
}
