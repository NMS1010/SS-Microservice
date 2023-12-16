namespace SS_Microservice.Contracts.Commands.Basket
{
    public interface IClearBasketCommand
    {
        string UserId { get; set; }
        List<long> VariantIds { get; set; }
    }
}
