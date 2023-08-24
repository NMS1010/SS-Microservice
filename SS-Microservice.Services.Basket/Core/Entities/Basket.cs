namespace SS_Microservice.Services.Basket.Core.Entities
{
    public class Basket
    {
        public string UserId { get; set; }
        public int BasketId { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}