using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Features.Order.Queries
{
    public class GetAllOrderQuery : GetOrderPagingRequest, IRequest<PaginatedResult<OrderDto>>
    {
    }
}