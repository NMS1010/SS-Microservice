﻿namespace SS_Microservice.Services.Order.Application.Models.OrderItem
{
    public class CreateOrderItemRequest
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}