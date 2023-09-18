﻿using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Interfaces;

namespace SS_Microservice.Services.Order.Application.Features.Order.Handlers
{
    public class GetAllOrderHandler : IRequestHandler<GetAllOrderQuery, PaginatedResult<OrderDto>>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;

        public GetAllOrderHandler(IOrderService orderService, IProductGrpcService productGrpcService)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
        }

        public async Task<PaginatedResult<OrderDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetOrderList(request);
            orders.Items.ForEach(o =>
            {
                o.OrderItems.ForEach(async oi =>
                {
                    var product = await _productGrpcService.GetProductById(oi.ProductId);
                    oi.ProductImage = product.MainImage;
                });
            });
            return orders;
        }
    }
}