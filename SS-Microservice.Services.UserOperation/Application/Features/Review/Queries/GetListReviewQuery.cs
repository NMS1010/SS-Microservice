using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Models.Review;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Order;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.User;

namespace SS_Microservice.Services.UserOperation.Application.Features.Review.Queries
{
    public class GetListReviewQuery : GetReviewPagingRequest, IRequest<PaginatedResult<ReviewDto>>
    {
    }

    public class GetListReviewHandler : IRequestHandler<GetListReviewQuery, PaginatedResult<ReviewDto>>
    {
        private readonly IReviewService _reviewService;
        private readonly IProductClientAPI _productClientAPI;
        private readonly IUserClientAPI _userClientAPI;
        private readonly IOrderClientAPI _orderClientAPI;

        public GetListReviewHandler(IReviewService reviewService, IProductClientAPI productClientAPI, IUserClientAPI userClientAPI, IOrderClientAPI orderClientAPI)
        {
            _reviewService = reviewService;
            _productClientAPI = productClientAPI;
            _userClientAPI = userClientAPI;
            _orderClientAPI = orderClientAPI;
        }

        public async Task<PaginatedResult<ReviewDto>> Handle(GetListReviewQuery request, CancellationToken cancellationToken)
        {
            var res = await _reviewService.GetListReview(request);
            foreach (var item in res.Items)
            {
                var product = await _productClientAPI.GetProduct(item.ProductId)
                    ?? throw new NotFoundException("Cannot find product");

                var user = await _userClientAPI.GetUser(item.UserId)
                    ?? throw new NotFoundException("Cannot find user");

                var orderItem = await _orderClientAPI.GetOrderItem(item.OrderItemId)
                    ?? throw new NotFoundException("Cannot find order item");

                item.Product = product;
                item.User = user;
                item.VariantName = orderItem.VariantName;
            }

            return res;
        }
    }
}
