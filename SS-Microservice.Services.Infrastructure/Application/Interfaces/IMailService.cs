using SS_Microservice.Services.Infrastructure.Application.Model.Mail;

namespace SS_Microservice.Services.Infrastructure.Application.Interfaces
{
    public interface IMailService
    {
        void SendMail(CreateMailRequest request);
    }
}