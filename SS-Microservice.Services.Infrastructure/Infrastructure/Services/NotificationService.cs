using AutoMapper;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Repository;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Specifications;
using SS_Microservice.Services.Infrastructure.Domain.Entities;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public NotificationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateNotification(CreateNotificationCommand command)
        {
            var notification = _mapper.Map<Notification>(command);
            await _unitOfWork.Repository<Notification>().Insert(notification);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot create this entity");
            }

            return isSuccess;
        }

        public async Task<PaginatedResult<NotificationDto>> GetAllNotification(GetAllNotificationQuery query)
        {
            var notiSpec = new NotifcationSpecification(query, isPaging: true);
            var countSpec = new NotifcationSpecification(query);
            var notifications = await _unitOfWork.Repository<Notification>().ListAsync(notiSpec);
            var count = await _unitOfWork.Repository<Notification>().CountAsync(countSpec);
            var notificationDtos = new List<NotificationDto>();
            notifications.ForEach(x => notificationDtos.Add(_mapper.Map<NotificationDto>(x)));

            return new PaginatedResult<NotificationDto>(notificationDtos, (int)query.PageIndex, count, (int)query.PageSize);
        }

        public async Task<bool> UpdateNotificationStatus(long notificationId)
        {
            var notification = await _unitOfWork.Repository<Notification>().GetById(notificationId);
            notification.Status = false;

            _unitOfWork.Repository<Notification>().Update(notification);

            var isSuccess = await _unitOfWork.Save() > 0;
            if (!isSuccess)
            {
                throw new Exception("Cannot update this entity");
            }
            return isSuccess;
        }
    }
}