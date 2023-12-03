using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Order;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.User;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetReviewQuery : IRequest<ReviewDto>
    {
        public long Id { get; set; }
    }

    public class GetReviewHandler : IRequestHandler<GetReviewQuery, ReviewDto>
    {
        private readonly IReviewService _reviewService;
        private readonly IProductClientAPI _productClientAPI;
        private readonly IUserClientAPI _userClientAPI;
        private readonly IOrderClientAPI _orderClientAPI;

        public GetReviewHandler(IReviewService reviewService, IOrderClientAPI orderClientAPI, IUserClientAPI userClientAPI, IProductClientAPI productClientAPI)
        {
            _reviewService = reviewService;
            _orderClientAPI = orderClientAPI;
            _userClientAPI = userClientAPI;
            _productClientAPI = productClientAPI;
        }

        public async Task<ReviewDto> Handle(GetReviewQuery request, CancellationToken cancellationToken)
        {
            var res = await _reviewService.GetReview(request);


            var product = await _productClientAPI.GetProduct(res.ProductId)
                ?? throw new NotFoundException("Cannot find product");

            var user = await _userClientAPI.GetUser(res.UserId)
                ?? throw new NotFoundException("Cannot find user");

            var orderItem = await _orderClientAPI.GetOrderItem(res.OrderItemId)
                ?? throw new NotFoundException("Cannot find order item");

            res.Product = product;
            res.User = user;
            res.VariantName = orderItem.VariantName;

            return res;
        }
    }
}
