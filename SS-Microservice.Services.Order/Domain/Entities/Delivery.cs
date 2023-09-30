using SS_Microservice.Common.Entities;

namespace SS_Microservice.Services.Order.Domain.Entities
{
    public class Delivery : BaseAuditableEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}