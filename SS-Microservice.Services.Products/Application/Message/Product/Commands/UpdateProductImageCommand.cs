﻿using MediatR;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Message.Product.Commands
{
    public class UpdateProductImageCommand : ProductImageUpdateRequest, IRequest<bool>
    {
    }
}