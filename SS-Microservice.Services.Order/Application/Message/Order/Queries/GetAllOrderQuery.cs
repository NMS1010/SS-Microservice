﻿using MediatR;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Models.Order;

namespace SS_Microservice.Services.Order.Application.Message.Order.Queries
{
    public class GetAllOrderQuery : OrderGetPagingRequest, IRequest<PaginatedResult<OrderDto>>
    {
    }
}