namespace SS_Microservice.Services.Inventory.Application.Models.Docket
{
    public class GetListDocketByDateRequest
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
