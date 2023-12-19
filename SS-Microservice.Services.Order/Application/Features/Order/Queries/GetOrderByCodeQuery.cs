﻿using AutoMapper;
using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Infrastructure.Services.Address;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetOrderByCodeQuery : IRequest<OrderDto>
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }

    public class GetOrderByCodeHandler : IRequestHandler<GetOrderByCodeQuery, OrderDto>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;
        private readonly IMapper _mapper;
        private readonly IAddressClientAPI _addressClientAPI;

        public GetOrderByCodeHandler(IOrderService orderService, IProductGrpcService productGrpcService,
            IMapper mapper, IAddressClientAPI addressClientAPI)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
            _mapper = mapper;
            _addressClientAPI = addressClientAPI;
        }

        public async Task<OrderDto> Handle(GetOrderByCodeQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrderByCode(request);

            var userAddressResp = await _addressClientAPI.GetDefaultAddress(request.UserId);
            if (userAddressResp == null || userAddressResp.Data == null)
            {
                throw new InternalServiceCommunicationException("Cannot get default user's address");
            }
            order.Address = userAddressResp.Data;

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

            return order;
        }
    }
}
