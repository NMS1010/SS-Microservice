namespace SS_Microservice.Services.Order.Infrastructure.Services.Inventory.Model.Request
{
    public class GetDocketByDateRequest
    {
        public List<DocketByDateItem> Items { get; set; }
    }
    public class DocketByDateItem
    {
        public string Type { get; set; }
        public DateTime FirstDate { get; set; }
        public DateTime LastDate { get; set; }
    }
}
