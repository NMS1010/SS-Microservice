using MailKit.Security;
using MimeKit;
using SS_Microservice.Common.Configuration;
using SS_Microservice.Services.Infrastructure.Application.Common.Constants;
using SS_Microservice.Services.Infrastructure.Application.Common.Options;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Model.Mail;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Services
{
    public class MailService : IMailService
    {
        private readonly string _mailTemplate;
        private readonly IConfiguration _configuration;

        private const string EMAIL_TEMPLATE = "email-template";

        public MailService(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _mailTemplate = Path.Combine(webHostEnvironment.WebRootPath, EMAIL_TEMPLATE);
            _configuration = configuration;
        }

        private string GetMailContent(CreateMailRequest request)
        {
            var path = Path.Combine(_mailTemplate, request.Type);
            string body = string.Empty;
            using (StreamReader reader = new(path))
            {
                body = reader.ReadToEnd();
            }
            foreach (var payload in request.Payloads)
            {
                body = body.Replace($"{{{payload.Key}}}", payload.Value);
            }
            //body = body.Replace("{name}", request.Name);
            //body = body.Replace("{email}", request.Email);
            //body = body.Replace("{OTP}", request.OTP);
            //if (request.OrderConfirmationMail != null)
            //{
            //    body = body.Replace("{email}", request.OrderConfirmationMail.Email);
            //    body = body.Replace("{receiver}", request.OrderConfirmationMail.Receiver);
            //    body = body.Replace("{phone}", request.OrderConfirmationMail.Phone);
            //    body = body.Replace("{address}", request.OrderConfirmationMail.Address);
            //    body = body.Replace("{paymentMethod}", request.OrderConfirmationMail.PaymentMethod);

            //    string totalPrice = request.OrderConfirmationMail.TotalPrice.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN"));
            //    body = body.Replace("{totalPrice}", totalPrice + "đ");
            //}

            return body;
        }

        public void SendMail(CreateMailRequest request)
        {
            try
            {
                //var options = _configuration.GetOptions<MailJetOptions>("MailJet");
                //MailjetClient client = new(options.PublicAPIKey, options.PrivateAPIKey);
                //MailjetRequest request = new MailjetRequest
                //{
                //    Resource = Send.Resource,
                //}
                //   .Property(Send.FromEmail, options.SendFromEmail)
                //   .Property(Send.FromName, options.SendFromName)
                //   .Property(Send.Subject, title)
                //   .Property(Send.HtmlPart, content)
                //   .Property(Send.Recipients, new JArray {
                //            new JObject {
                //                 {"Email", email},
                //                 {"Name", name}
                //            }
                //       });

                //_ = Task.Run(() => client.PostAsync(request));

                var options = _configuration.GetOptions<MailSettingOptions>("MailSetting");
                var mailMessage = new MimeMessage
                {
                    Sender = new MailboxAddress(options.DisplayName, options.Mail)
                };

                mailMessage.From.Add(new MailboxAddress(options.DisplayName, options.Mail));
                mailMessage.To.Add(MailboxAddress.Parse(request.To));

                mailMessage.Subject = MAIL_TYPE.Subject[request.Type];

                var builder = new BodyBuilder
                {
                    HtmlBody = GetMailContent(request)
                };
                mailMessage.Body = builder.ToMessageBody();

                var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect(options.Host, options.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(options.Mail, options.Password);
                _ = Task.Run(() => smtp.Send(mailMessage));
            }
            catch
            {
                throw;
            }
        }
    }
}