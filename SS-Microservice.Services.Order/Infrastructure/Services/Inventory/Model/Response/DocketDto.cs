namespace SS_Microservice.Services.Order.Infrastructure.Services.Inventory.Model.Response
{
    public class DocketDto
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public long ProductId { get; set; }
        public long? OrderId { get; set; }
        public long Quantity { get; set; }
        public string Note { get; set; }
    }
}
