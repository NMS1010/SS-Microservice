using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class DeleteListReviewCommand : IRequest<bool>
    {
        public List<long> Ids { get; set; }
    }

    public class DeleteListReviewHandler : IRequestHandler<DeleteListReviewCommand, bool>
    {
        private readonly IReviewService _reviewService;

        public DeleteListReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<bool> Handle(DeleteListReviewCommand request, CancellationToken cancellationToken)
        {
            //pub event to product service to update review

            return await _reviewService.DeleteListReview(request);
        }
    }
}
