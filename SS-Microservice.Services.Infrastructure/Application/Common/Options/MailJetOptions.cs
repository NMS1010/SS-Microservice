namespace SS_Microservice.Services.Infrastructure.Application.Common.Options
{
    public class MailJetOptions
    {
        public string SendFromName { get; set; }
        public string SendFromEmail { get; set; }
        public string PublicAPIKey { get; set; }
        public string PrivateAPIKey { get; set; }
    }
}