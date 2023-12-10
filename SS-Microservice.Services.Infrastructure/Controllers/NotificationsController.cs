using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Infrastructure.Application.Dto;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Commands;
using SS_Microservice.Services.Infrastructure.Application.Features.Notification.Queries;
using SS_Microservice.Services.Infrastructure.Application.Model.Notification;

namespace SS_Microservice.Services.Infrastructure.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NotificationsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public NotificationsController(ISender sender, IMapper mapper, ICurrentUserService currentUserService)
        {
            _sender = sender;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        public async Task<IActionResult> GetListNotification([FromQuery] GetNotificationPagingRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var notifications = await _sender.Send(_mapper.Map<GetListNotificationQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<NotificationDto>>.Success(notifications, StatusCodes.Status200OK));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateNotification([FromRoute] long id)
        {
            var request = new UpdateNotificationRequest()
            {
                Id = id,
                UserId = _currentUserService.UserId
            };

            var res = await _sender.Send(_mapper.Map<UpdateNotificationCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpPut("all")]
        public async Task<IActionResult> UpdateListNotification()
        {
            var res = await _sender.Send(new UpdateListNotificationCommand() { UserId = _currentUserService.UserId });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }
    }
}