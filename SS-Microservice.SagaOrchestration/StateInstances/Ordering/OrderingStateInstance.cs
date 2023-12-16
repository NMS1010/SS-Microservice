using MassTransit;
using System.Text;

namespace SS_Microservice.SagaOrchestration.StateInstances.Ordering
{
    public class OrderingStateInstance : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public long OrderId { get; set; }
        public string OrderCode { get; set; }
        public string UserId { get; set; }
        public List<ProductInstance> ProductInstances { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            GetType().GetProperties().ToList().ForEach(p =>
            {
                sb.AppendLine($"{p.Name}: {p.GetValue(this, null)}");
            });

            sb.Append("------------------------");

            return sb.ToString();
        }
    }

    public class ProductInstance
    {
        public long ProductId { get; set; }
        public long VariantId { get; set; }
        public int Quantity { get; set; }
    }
}
