using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SS_Microservice.Common.Exceptions;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Infrastructure.Application.Common.SignalR;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Specifications.Notification;
using SS_Microservice.Services.Infrastructure.Domain.Entities;

namespace SS_Microservice.Services.Infrastructure.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<InfrastructureHub> _hub;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper, IHubContext<InfrastructureHub> hub)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hub = hub;
        }

        public async Task CreateOrderNotification(CreateNotificationCommand command)
        {
            var notification = _mapper.Map<Notification>(command);
            notification.UserId = command.UserId;
            await _unitOfWork.Repository<Notification>().Insert(notification);

            var res = await _unitOfWork.Save() > 0;
            if (res)
            {
                var count = await _unitOfWork.Repository<Notification>().CountAsync(new NotificationSpecification(command.UserId, false));
                await _hub.Clients.Group(command.UserId).SendAsync("ReceiveNotification", _mapper.Map<NotificationDto>(notification), count);
            }
        }

        public Task CreateSaleNotification(CreateNotificationCommand command)
        {
            throw new NotImplementedException();
            //var users = await _unitOfWork.Repository<AppUser>().GetAll();
            //foreach (var user in users)
            //{
            //    var notification = _mapper.Map<Notification>(command);
            //    notification.UserId = user;
            //    await _unitOfWork.Repository<Notification>().Insert(notification);
            //}

            //var res = await _unitOfWork.Save() > 0;
            //if (res)
            //{
            //    foreach (var user in users)
            //    {
            //        var count = await _unitOfWork.Repository<Notification>().CountAsync(new NotificationSpecification(command.UserId, false));
            //        await _hub.Clients.Group(user.Id).SendAsync("ReceiveNotification", _mapper.Map<NotificationDto>(_mapper.Map<Notification>(command)), count);
            //    }
            //}
        }

        public async Task<PaginatedResult<NotificationDto>> GetListNotification(GetListNotificationQuery query)
        {
            var notifications = await _unitOfWork.Repository<Notification>().ListAsync(new NotificationSpecification(query, isPaging: true));
            var total = await _unitOfWork.Repository<Notification>().CountAsync(new NotificationSpecification(query));

            return new PaginatedResult<NotificationDto>(notifications
                .Select(x => _mapper.Map<NotificationDto>(x)).ToList(),
                query.PageIndex, total, query.PageSize);
        }

        public async Task<bool> UpdateListNotification(UpdateListNotificationCommand command)
        {
            var notifications = await _unitOfWork.Repository<Notification>().ListAsync(new NotificationSpecification(command.UserId));

            notifications.ForEach(x =>
            {
                x.Status = true;
                _unitOfWork.Repository<Notification>().Update(x);
            });

            return await _unitOfWork.Save() > 0;
        }

        public async Task<bool> UpdateNotification(UpdateNotificationCommand command)
        {
            var notification = await _unitOfWork.Repository<Notification>().GetEntityWithSpec(new NotificationSpecification(command.UserId, command.Id))
                ?? throw new NotFoundException("Cannot find notification");

            notification.Status = true;

            _unitOfWork.Repository<Notification>().Update(notification);

            var res = await _unitOfWork.Save() > 0;

            var countNotify = await _unitOfWork.Repository<Notification>().CountAsync(new NotificationSpecification(command.UserId, false));
            await _hub.Clients.Group(command.UserId).SendAsync("CountUnreadingNotification", countNotify);

            return res;
        }
    }
}