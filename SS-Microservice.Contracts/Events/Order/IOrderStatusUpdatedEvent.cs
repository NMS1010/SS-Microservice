namespace SS_Microservice.Contracts.Events.Order
{
    public interface IOrderStatusUpdatedEvent
    {
        long OrderId { get; set; }
        string OrderCode { get; set; }
        string Image { get; set; }
        string UserId { get; set; }
        string Status { get; set; }
    }
}
