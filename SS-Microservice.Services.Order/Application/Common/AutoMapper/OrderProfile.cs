using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Delivery.Commands;
using SS_Microservice.Services.Order.Application.Features.Delivery.Queries;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;
using SS_Microservice.Services.Order.Application.Features.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Commands;
using SS_Microservice.Services.Order.Application.Features.PaymentMethod.Queries;
using SS_Microservice.Services.Order.Application.Features.Transaction.Queries;
using SS_Microservice.Services.Order.Application.Models.Delivery;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;
using SS_Microservice.Services.Order.Application.Models.OrderState;
using SS_Microservice.Services.Order.Application.Models.PaymentMethod;
using SS_Microservice.Services.Order.Application.Models.Transaction;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class OrderProfile : Profile
    {
        private const string USER_CONTENT_FOLDER = "user-content";

        public string GetFile(string filename, IHttpContextAccessor httpContextAccessor)
        {
            var req = httpContextAccessor.HttpContext.Request;
            var path = $"{req.Scheme}://{req.Host}/{USER_CONTENT_FOLDER}/{filename}";
            return path;
        }

        public OrderProfile(IHttpContextAccessor httpContextAccessor)
        {
            CreateMap<Domain.Entities.Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderState, OrderStateDto>();
            CreateMap<Transaction, TransactionDto>();
            CreateMap<Delivery, DeliveryDto>()
                .ForMember(des => des.Image,
                act => act.MapFrom(src => GetFile(src.Image, httpContextAccessor)));
            CreateMap<OrderCancellationReason, OrderCancellationReasonDto>();
            CreateMap<PaymentMethod, PaymentMethodDto>()
                .ForMember(des => des.Image,
                act => act.MapFrom(src => GetFile(src.Image, httpContextAccessor)));

            CreateMap<GetOrderPagingRequest, GetAllOrderQuery>();
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<UpdateOrderRequest, UpdateOrderCommand>();

            CreateMap<GetOrderStatePagingRequest, GetAllOrderStateQuery>();
            CreateMap<CreateOrderStateRequest, CreateOrderStateCommand>();
            CreateMap<UpdateOrderStateRequest, UpdateOrderStateCommand>();

            CreateMap<GetDeliveryPagingRequest, GetAllDeliveryQuery>();
            CreateMap<CreateDeliveryRequest, CreateDeliveryCommand>();
            CreateMap<UpdateDeliveryRequest, UpdateDeliveryCommand>();

            CreateMap<GetTransactionPagingRequest, GetAllTransactionQuery>();

            CreateMap<GetPaymentMethodPagingRequest, GetAllPaymentMethodQuery>();
            CreateMap<CreatePaymentMethodRequest, CreatePaymentMethodCommand>();
            CreateMap<UpdatePaymentMethodRequest, UpdatePaymentMethodCommand>();

            CreateMap<GetOrderCancellationReasonPagingRequest, GetAllOrderCancellationReasonQuery>();
            CreateMap<CreateOrderCancellationReasonRequest, CreateOrderCancellationReasonCommand>();
            CreateMap<UpdateOrderCancellationReasonRequest, UpdateOrderCancellationReasonCommand>();
        }
    }
}