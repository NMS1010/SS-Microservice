using MediatR;
using SS_Microservice.Contracts.Models;
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
        private readonly ISender _sender;

        public CreateReviewHandler(IReviewService reviewService, ISender sender)
        {
            _reviewService = reviewService;
            _sender = sender;
        }

        public async Task<long> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var reviewId = await _reviewService.CreateReview(request);
            //pub event to product service to update review

            if (reviewId > 0)
            {
                var productReview = await _reviewService.GetProductReview(reviewId);
                await _sender.Send(new UpdateProductRatingCommand()
                {
                    ProductRatings = new List<ProductRating>()
                {
                    new()
                    {
                        ProductId = productReview.ProductId,
                        Rating = productReview.Rating
                    }
                }
                });
            }

            return reviewId;
        }
    }
}
