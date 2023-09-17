﻿using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Order.Application.Models.Order
{
    public class GetOrderPagingRequest : PagingRequest
    {
        public string UserId { get; set; }
    }
}