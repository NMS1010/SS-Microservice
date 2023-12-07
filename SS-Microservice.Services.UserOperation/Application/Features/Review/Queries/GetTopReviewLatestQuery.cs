using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Order;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.User;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetTopReviewLatestQuery : IRequest<List<ReviewDto>>
    {
        public int Top { get; set; } = 5;
    }

    public class GetTopReviewLatestQueryHandler : IRequestHandler<GetTopReviewLatestQuery, List<ReviewDto>>
    {
        private readonly IReviewService _reviewService;
        private readonly IProductClientAPI _productClientAPI;
        private readonly IUserClientAPI _userClientAPI;
        private readonly IOrderClientAPI _orderClientAPI;

        public GetTopReviewLatestQueryHandler(IReviewService reviewService, IOrderClientAPI orderClientAPI,
            IUserClientAPI userClientAPI, IProductClientAPI productClientAPI)
        {
            _reviewService = reviewService;
            _orderClientAPI = orderClientAPI;
            _userClientAPI = userClientAPI;
            _productClientAPI = productClientAPI;
        }

        public async Task<List<ReviewDto>> Handle(GetTopReviewLatestQuery request, CancellationToken cancellationToken)
        {
            var res = await _reviewService.GetTopReviewLatest(request);

            foreach (var item in res)
            {
                var productResp = await _productClientAPI.GetProduct(item.ProductId);
                if (productResp == null || productResp.Data == null)
                    throw new InternalServiceCommunicationException("Get product failed");

                var userResp = await _userClientAPI.GetUser(item.UserId);
                if (userResp == null || userResp.Data == null)
                    throw new InternalServiceCommunicationException("Get user failed");

                var orderItemResp = await _orderClientAPI.GetOrderItem(item.OrderItemId);
                if (orderItemResp == null || orderItemResp.Data == null)
                    throw new InternalServiceCommunicationException("Get order item failed");

                item.Product = productResp.Data;
                item.User = userResp.Data;
                item.VariantName = orderItemResp.Data.VariantName;
            }

            return res;
        }
    }
}
