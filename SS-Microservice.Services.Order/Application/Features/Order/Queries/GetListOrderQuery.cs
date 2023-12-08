﻿using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetListOrderQuery : GetOrderPagingRequest, IRequest<PaginatedResult<OrderDto>>
    {
    }

    public class GetListOrderHandler : IRequestHandler<GetListOrderQuery, PaginatedResult<OrderDto>>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;

        public GetListOrderHandler(IOrderService orderService, IProductGrpcService productGrpcService)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
        }

        public async Task<PaginatedResult<OrderDto>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetListOrder(request);
            //orders.Items.ForEach(o =>
            //{
            //    o.OrderItems.ForEach(async oi =>
            //    {
            //        var product = await _productGrpcService.GetProductByVariantId(oi.VariantId);
            //        oi.ProductImage = product.ProductImage;
            //    });
            //});
            return orders;
        }
    }
}