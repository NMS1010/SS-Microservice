using AutoMapper;
using MediatR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Infrastructure.Services.Address;
using SS_Microservice.Services.Order.Infrastructure.Services.Auth;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetOrderQuery : IRequest<OrderDto>
    {
        public long Id { get; set; }
    }

    public class GetOrderHandler : IRequestHandler<GetOrderQuery, OrderDto>
    {
        private readonly IOrderService _orderService;
        private readonly IProductGrpcService _productGrpcService;
        private readonly IMapper _mapper;
        private readonly IAddressClientAPI _addressClientAPI;
        private readonly IAuthClientAPI _authClientAPI;

        public GetOrderHandler(IOrderService orderService, IProductGrpcService productGrpcService,
            IMapper mapper, IAddressClientAPI addressClientAPI, IAuthClientAPI authClientAPI)
        {
            _orderService = orderService;
            _productGrpcService = productGrpcService;
            _mapper = mapper;
            _addressClientAPI = addressClientAPI;
            _authClientAPI = authClientAPI;
        }

        public async Task<OrderDto> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetOrder(request);
            var userAddressResp = await _addressClientAPI.GetAddress(order.AddressId);
            if (userAddressResp == null || userAddressResp.Data == null)
            {
                throw new InternalServiceCommunicationException("Cannot get user's address");
            }
            var userResp = await _authClientAPI.GetUser(userAddressResp.Data.UserId);
            if (userResp == null || userResp.Data == null)
            {
                throw new InternalServiceCommunicationException("Cannot get user");
            }
            order.Address = userAddressResp.Data;
            order.User = userResp.Data;

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