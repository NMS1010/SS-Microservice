using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Models.Order;
using SS_Microservice.Services.Order.Application.Models.OrderState;

namespace SS_Microservice.Services.Order.Application.Message.OrderState.Queries
{
    public class GetAllOrderStateQuery : OrderStateGetPagingRequest, IRequest<PaginatedResult<OrderStateDto>>
    {
    }
}