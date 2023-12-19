using MassTransit;
using MediatR;
using SS_Microservice.Contracts.Events.Order;
using SS_Microservice.Services.Infrastructure.Application.Common.Constants;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.Order
{
    public class OrderStatusUpdatedEventConsumer : IConsumer<IOrderStatusUpdatedEvent>
    {
        private readonly ISender _sender;

        public OrderStatusUpdatedEventConsumer(ISender sender)
        {
            _sender = sender;
        }

        public async Task Consume(ConsumeContext<IOrderStatusUpdatedEvent> context)
        {
            var notificationCommand = new CreateOrderNotificationCommand
            {
                UserId = context.Message.UserId,
                Title = "Cập nhật đơn hàng",
                Content = $"Đơn hàng #{context.Message.OrderCode} của bạn đã chuyển sang trạng thái " + context.Message.Status,
                Anchor = "/user/order/" + context.Message.OrderCode,
                Status = false,
                Type = NOTIFICATION_TYPE.ORDER,
                Image = context.Message.Image
            };

            await _sender.Send(notificationCommand);
        }
    }
}
