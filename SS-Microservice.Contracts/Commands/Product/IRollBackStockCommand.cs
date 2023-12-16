using SS_Microservice.Common.Types.Messages;
using SS_Microservice.Contracts.Models;

namespace SS_Microservice.Contracts.Commands.Product
{
    public interface IRollBackStockCommand : ICommand
    {
        List<ProductStock> Stocks { get; set; }
    }
}
