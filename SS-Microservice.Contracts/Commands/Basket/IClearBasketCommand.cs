using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Commands.Basket
{
    public interface IClearBasketCommand : ICommand
    {
        string UserId { get; set; }
        List<long> VariantIds { get; set; }
    }
}
