﻿using SS_Microservice.Common.Model.Paging;

namespace SS_Microservice.Services.Auth.Infrastructure.Services.Address.Model.Request
{
    public class GetListAddressRequest : PagingRequest
    {
        public string UserId { get; set; }
    }
}
