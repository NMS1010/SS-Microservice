using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using SS_Microservice.SagaOrchestration.StateMaps.Order;

namespace SS_Microservice.SagaOrchestration.DbContext
{
    public class SagaAppDBContext : SagaDbContext
    {
        public SagaAppDBContext(DbContextOptions options) : base(options)
        {
        }
        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new OrderingStateMap(); }
        }
    }
}
