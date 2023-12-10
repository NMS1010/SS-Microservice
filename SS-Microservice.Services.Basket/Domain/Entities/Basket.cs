using SS_Microservice.Common.Types.Entities;

namespace SS_Microservice.Services.Basket.Domain.Entities
{
    public class Basket : BaseAuditableEntity<long>
    {
        public string UserId { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}