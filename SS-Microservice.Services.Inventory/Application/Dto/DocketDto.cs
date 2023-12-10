using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Inventory.Application.Dto
{
    public class DocketDto : BaseAuditableEntity<long>
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public long? OrderId { get; set; }
        public long Quantity { get; set; }
        public string Note { get; set; }
    }
}
