using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderCancellationReason.Queries;
using SS_Microservice.Services.Order.Application.Models.OrderCancellationReason;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class OrderCancellationReasonProfile : Profile
    {
        public OrderCancellationReasonProfile()
        {
            CreateMap<OrderCancellationReason, OrderCancellationReasonDto>();

            CreateMap<CreateOrderCancellationReasonCommand, OrderCancellationReason>();
            CreateMap<UpdateOrderCancellationReasonCommand, OrderCancellationReason>();

            // mapping request - command
            CreateMap<CreateOrderCancellationReasonRequest, CreateOrderCancellationReasonCommand>();
            CreateMap<UpdateOrderCancellationReasonRequest, UpdateOrderCancellationReasonCommand>();
            CreateMap<GetOrderCancellationReasonPagingRequest, GetListOrderCancellationReasonQuery>();
        }
    }
}
