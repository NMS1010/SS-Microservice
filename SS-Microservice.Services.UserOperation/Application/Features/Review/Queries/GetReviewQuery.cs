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


            var productResp = await _productClientAPI.GetProduct(res.ProductId);
            if (productResp == null || productResp.Data == null)
                throw new InternalServiceCommunicationException("Get product failed");

            var userResp = await _userClientAPI.GetUser(res.UserId);
            if (userResp == null || userResp.Data == null)
                throw new InternalServiceCommunicationException("Get user failed");

            var orderItemResp = await _orderClientAPI.GetOrderItem(res.OrderItemId);
            if (orderItemResp == null || orderItemResp.Data == null)
                throw new InternalServiceCommunicationException("Get order item failed");

            res.Product = productResp.Data;
            res.User = userResp.Data;
            res.VariantName = orderItemResp.Data.VariantName;

            return res;
        }
    }
}
