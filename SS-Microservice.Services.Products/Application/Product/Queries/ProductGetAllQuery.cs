﻿using MediatR;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Model.Product;

namespace SS_Microservice.Services.Products.Application.Product.Queries
{
    public class ProductGetAllQuery : ProductPagingRequest, IRequest<List<ProductDTO>>
    {
    }
}