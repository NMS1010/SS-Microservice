using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Commands.Mail;

namespace SS_Microservice.Services.Infrastructure.Application.Interfaces
{
    public interface IMailService
    {
        void SendMail(SendMailCommand command);
    }
}