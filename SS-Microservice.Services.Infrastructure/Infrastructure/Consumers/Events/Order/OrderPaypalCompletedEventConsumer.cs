using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Infrastructure.Application.Common.Constants;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Commands.Mail;
using System.Globalization;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.Order
{
    public class OrderPaypalCompletedEventConsumer : IConsumer<IOrderPaypalCompletedEvent>
    {
        private readonly ISender _sender;
        private readonly IMailService _mailService;

        public OrderPaypalCompletedEventConsumer(ISender sender, IMailService mailService)
        {
            _sender = sender;
            _mailService = mailService;
        }
        public async Task Consume(ConsumeContext<IOrderPaypalCompletedEvent> context)
        {
            var notificationCommand = new CreateOrderNotificationCommand
            {
                UserId = context.Message.UserId,
                Title = "Thanh toán đơn hàng",
                Content = $"Đơn hàng #{context.Message.OrderCode} của bạn đã thanh toán thành công và đang được hệ thống xử lý ",
                Anchor = "/user/order/" + context.Message.OrderCode,
                Status = false,
                Type = NOTIFICATION_TYPE.ORDER,
                Image = context.Message.Image
            };

            await _sender.Send(notificationCommand);


            var payloads = new Dictionary<string, string>()
                {
                    { "address", context.Message.Address},
                    { "receiver", context.Message.Receiver},
                    { "phone", context.Message.Phone},
                    { "email", context.Message.ReceiverEmail},
                    { "name", context.Message.UserName },
                    { "paymentMethod", context.Message.PaymentMethod },
                    { "totalPrice", context.Message.TotalPrice.ToString("#,###", CultureInfo.GetCultureInfo("vi-VN")) + "đ" }
                };

            var sendMailCommand = new SendMailCommand()
            {
                To = context.Message.Email,
                Type = MAIL_TYPE.ORDER_CONFIRMATION,
                Payloads = payloads
            };
            _mailService.SendMail(sendMailCommand);
        }
    }
}
