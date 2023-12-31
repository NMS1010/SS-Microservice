﻿using SS_Microservice.Common.Specifications;
using SS_Microservice.Services.Order.Domain.Entities;

namespace SS_Microservice.Services.Order.Application.Specifications.Order
{
    public class OrderItemSpecification : BaseSpecification<OrderItem>
    {
        public OrderItemSpecification(long orderId)
            : base(x => x.Order.Id == orderId)
        {
        }

        public OrderItemSpecification(long orderItemId, string status)
            : base(x => x.Id == orderItemId && x.Order.Status == status)
        {
        }

        public OrderItemSpecification(long variantId, DateTime firstDate, DateTime lastDate, string status)
            : base(
                  x => x.VariantId == variantId
                  && x.Order.CreatedAt >= firstDate
                  && x.Order.CreatedAt <= lastDate
                  && x.Order.Status == status)
        { }

    }
}