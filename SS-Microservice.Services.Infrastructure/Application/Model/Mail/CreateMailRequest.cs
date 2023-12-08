namespace SS_Microservice.Services.Infrastructure.Application.Model.Mail
{
    public class CreateMailRequest
    {
        public string To { get; set; }
        public string Type { get; set; }

        public IDictionary<string, string> Payloads { get; set; }
    }
}
