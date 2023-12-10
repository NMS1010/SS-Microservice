using SS_Microservice.Common.Types.Messages;

namespace SS_Microservice.Contracts.Commands.Mail
{
    public interface ISendMailCommand : ICommand
    {
        string To { get; set; }
        string Type { get; set; }

        IDictionary<string, string> Payloads { get; set; }
    }
}
