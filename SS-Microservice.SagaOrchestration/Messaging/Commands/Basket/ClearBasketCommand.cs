using SS_Microservice.Contracts.Commands.Basket;

namespace SS_Microservice.SagaOrchestration.Messaging.Commands.Basket
{
    public class ClearBasketCommand : IClearBasketCommand
    {
        public Guid CorrelationId { get; set; }
        public string UserId { get; set; }
        public List<long> VariantIds { get; set; }
    }
}
