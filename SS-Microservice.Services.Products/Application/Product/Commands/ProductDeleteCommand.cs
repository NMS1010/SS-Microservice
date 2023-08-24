﻿using MediatR;

namespace SS_Microservice.Services.Products.Application.Product.Commands
{
    public class ProductDeleteCommand : IRequest<bool>
    {
        public string ProductId { get; set; }
    }
}