﻿using AutoMapper;
using MediatR;
using SS_Microservice.Common.Types.Model.Paging;
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
        private readonly IMapper _mapper;

        public GetListOrderHandler(IOrderService orderService, IProductGrpcService productGrpcService, IMapper mapper)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<OrderDto>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderService.GetListOrder(request);
            foreach (var order in orders.Items)
            {
                foreach (var item in order.Items)
                {
                    var product = await _productGrpcService.GetProductByVariant(
                        new SS_Microservice.Common.Grpc.Product.Protos.GetProductByVariant()
                        {
                            VariantId = item.VariantId
                        }
                    );
                    _mapper.Map(product, item);
                }
            }

            return orders;
        }
    }
}