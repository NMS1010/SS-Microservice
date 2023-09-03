using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Message.Order.Commands;
using SS_Microservice.Services.Order.Application.Message.Order.Queries;
using SS_Microservice.Services.Order.Application.Message.OrderState.Commands;
using SS_Microservice.Services.Order.Application.Message.OrderState.Queries;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Application.Models.OrderState;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class OrderProfile : Profile
    {
        protected OrderProfile()
        {
            CreateMap<Core.Entities.Order, OrderDto>();
            CreateMap<Core.Entities.OrderItem, OrderItemDto>();
            CreateMap<Core.Entities.OrderState, OrderStateDto>();
            CreateMap<GetOrderPagingRequest, GetAllOrderQuery>();
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<UpdateOrderRequest, UpdateOrderCommand>();

            CreateMap<GetOrderStatePagingRequest, GetAllOrderStateQuery>();
            CreateMap<CreateOrderStateRequest, CreateOrderStateCommand>();
            CreateMap<UpdateOrderStateRequest, UpdateOrderStateCommand>();
        }
    }
}