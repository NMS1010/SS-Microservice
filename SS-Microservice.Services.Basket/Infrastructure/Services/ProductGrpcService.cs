﻿using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Services.Basket.Core;
using SS_Microservice.Services.Basket.Core.Interfaces;

namespace SS_Microservice.Services.Basket.Infrastructure.Services
{
    public class ProductGrpcService : IProductGrpcService
    {
        private readonly ProductProtoService.ProductProtoServiceClient _productProtoServiceClient;
        private readonly ILogger<ProductGrpcService> _logger;

        public ProductGrpcService(ProductProtoService.ProductProtoServiceClient productProtoServiceClient, ILogger<ProductGrpcService> logger)
        {
            _productProtoServiceClient = productProtoServiceClient;
            _logger = logger;
        }

        public async Task<ProductResponse> GetProductById(GetProductDetail request)
        {
            _logger.LogInformation("Starting gRPC connection from Basket to Product Service");
            var product = await _productProtoServiceClient.GetProductInformationAsync(request);
            if (product == null)
            {
                _logger.LogError("Failed to get result from gRPC service");
            }
            else
            {
                _logger.LogInformation("Get result successfully from gRPC service");
            }
            return product;
        }
    }
}