using MediatR;
using SS_Microservice.Contracts.Models;
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
        private readonly ISender _sender;

        public DeleteReviewHandler(IReviewService reviewService, MediatR.ISender sender)
        {
            _reviewService = reviewService;
            _sender = sender;
        }

        public async Task<bool> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            //pub event to product service to update review

            var isSuccess = await _reviewService.DeleteReview(request);

            if (isSuccess)
            {
                var productReview = await _reviewService.GetProductReview(request.Id);
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

            return isSuccess;
        }
    }
}
