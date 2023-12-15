using AutoMapper;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Order.Commands;
using SS_Microservice.Services.Order.Application.Features.Order.Queries;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Common.AutoMapper
{
    public class OrderProfile : Profile
    {

        public OrderProfile()
        {
            CreateMap<Domain.Entities.Order, OrderDto>();

            // mapping request - command
            CreateMap<CreateOrderRequest, CreateOrderCommand>();
            CreateMap<UpdateOrderRequest, UpdateOrderCommand>();
            CreateMap<CompletePaypalOrderRequest, CompletePaypalOrderCommand>();
            CreateMap<GetOrderPagingRequest, GetListUserOrderQuery>();
            CreateMap<GetOrderPagingRequest, GetListOrderQuery>();
        }
    }
}