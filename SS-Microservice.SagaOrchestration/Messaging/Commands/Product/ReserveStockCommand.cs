﻿using SS_Microservice.Contracts.Commands.Product;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.SagaOrchestration.Messaging.Commands.Product
{
    public class ReserveStockCommand : IReserveStockCommand
    {
        public Guid CorrelationId { get; set; }
        public List<ProductStock> Stocks { get; set; }
    }
}
