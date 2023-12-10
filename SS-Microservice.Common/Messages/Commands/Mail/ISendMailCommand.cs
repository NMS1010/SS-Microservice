namespace SS_Microservice.Common.Messages.Commands.Mail
{
    public interface ISendMailCommand : ICommand
    {
        string To { get; set; }
        string Type { get; set; }

        IDictionary<string, string> Payloads { get; set; }
    }
}
