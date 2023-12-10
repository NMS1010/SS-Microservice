using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Inventory.Domain.Entities
{
    public class Docket : BaseAuditableEntity<long>
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public string Note { get; set; }
        public long Quantity { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
    }
}