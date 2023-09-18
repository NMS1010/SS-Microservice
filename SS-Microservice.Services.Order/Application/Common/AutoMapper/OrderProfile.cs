using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Features.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Features.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Application.Models.OrderState;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class OrderProfile : Profile
    {
        protected OrderProfile()
        {
            CreateMap<Domain.Entities.Order, OrderDto>();
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderState, OrderStateDto>();
            CreateMap<GetOrderPagingRequest, GetAllOrderQuery>();
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<UpdateOrderRequest, UpdateOrderCommand>();

            CreateMap<GetOrderStatePagingRequest, GetAllOrderStateQuery>();
            CreateMap<CreateOrderStateRequest, CreateOrderStateCommand>();
            CreateMap<UpdateOrderStateRequest, UpdateOrderStateCommand>();
        }
    }
}