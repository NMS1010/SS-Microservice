﻿using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Products.Application.Model.Product
{
    public class GetProductImagePagingRequest : PagingRequest
    {
        public string ProductId { get; set; }
    }
}