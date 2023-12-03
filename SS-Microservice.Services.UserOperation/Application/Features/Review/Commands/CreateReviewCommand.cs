using MediatR;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Commands
{
    public class CreateReviewCommand : CreateReviewRequest, IRequest<long>
    {
    }

    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, long>
    {
        private readonly IReviewService _reviewService;

        public CreateReviewHandler(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        public async Task<long> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            //pub event to product service to update review

            return await _reviewService.CreateReview(request);
        }
    }
}
