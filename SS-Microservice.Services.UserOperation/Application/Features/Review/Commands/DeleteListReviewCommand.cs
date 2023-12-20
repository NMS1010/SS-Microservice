using MediatR;
using SS_Microservice.Contracts.Models;
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
        private readonly ISender _sender;

        public DeleteListReviewHandler(IReviewService reviewService, ISender sender)
        {
            _reviewService = reviewService;
            _sender = sender;
        }

        public async Task<bool> Handle(DeleteListReviewCommand request, CancellationToken cancellationToken)
        {
            //pub event to product service to update review
            var isSuccess = await _reviewService.DeleteListReview(request);
            if (isSuccess)
            {
                var productRatings = new List<ProductRating>();
                foreach (var id in request.Ids)
                {
                    var productReview = await _reviewService.GetProductReview(id);
                    productRatings.Add(new ProductRating()
                    {
                        ProductId = productReview.ProductId,
                        Rating = productReview.Rating
                    });
                }

                await _sender.Send(new UpdateProductRatingCommand()
                {
                    ProductRatings = productRatings
                });
            }

            return isSuccess;
        }
    }
}
