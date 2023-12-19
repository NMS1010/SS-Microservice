using SS_Microservice.Contracts.Commands.Mail;

namespace SS_Microservice.Services.Auth.Application.Features.Mail.Command
{
    public class SendMailCommand : ISendMailCommand
    {
        public string To { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string> Payloads { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
