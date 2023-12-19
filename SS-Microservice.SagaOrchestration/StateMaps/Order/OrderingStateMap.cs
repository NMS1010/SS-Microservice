using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SS_Microservice.SagaOrchestration.StateInstances.Ordering;

namespace SS_Microservice.SagaOrchestration.StateMaps.Order
{
    public class OrderingStateMap : SagaClassMap<OrderingStateInstance>
    {
        protected override void Configure(EntityTypeBuilder<OrderingStateInstance> entity, ModelBuilder model)
        {
            base.Configure(entity, model);
            entity.HasMany(c => c.ProductInstances).WithOne(x => x.OrderingStateInstance);
        }
    }
}
