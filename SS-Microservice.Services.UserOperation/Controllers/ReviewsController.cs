using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Attributes;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Commands;
using SS_Microservice.Services.UserOperation.Application.Features.Review.Queries;
using SS_Microservice.Services.UserOperation.Application.Models.Review;

namespace SS_Microservice.Services.UserOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReviewsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public ReviewsController(ISender sender, ICurrentUserService currentUserService, IMapper mapper)
        {
            _sender = sender;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListReview([FromQuery] GetReviewPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListReviewQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<ReviewDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("top5-review-latest")]
        public async Task<IActionResult> GetTop5ReviewLatest()
        {
            var res = await _sender.Send(new GetTopReviewLatestQuery());

            return Ok(CustomAPIResponse<List<ReviewDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReview([FromRoute] long id)
        {
            var res = await _sender.Send(new GetReviewQuery() { Id = id });

            return Ok(CustomAPIResponse<ReviewDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("order-item/{orderItemId}")]
        public async Task<IActionResult> GetReviewByOrderItem([FromRoute] long orderItemId)
        {
            var res = await _sender.Send(new GetReviewByOrderItemQuery()
            {
                OrderItemId = orderItemId,
                UserId = _currentUserService.UserId
            });

            return Ok(CustomAPIResponse<ReviewDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromForm] CreateReviewRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var reviewId = await _sender.Send(_mapper.Map<CreateReviewCommand>(request));

            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/reviews/{reviewId}";

            return Created(url, CustomAPIResponse<object>.Success(new { id = reviewId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview([FromForm] UpdateReviewRequest request, [FromRoute] long id)
        {
            request.Id = id;
            request.UserId = _currentUserService.UserId;

            var res = await _sender.Send(_mapper.Map<UpdateReviewCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpPut("reply/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ReplyReview([FromBody] ReplyReviewRequest request, [FromRoute] long id)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<ReplyReviewCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpGet("count/{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> CountReview([FromRoute] long productId)
        {
            var res = await _sender.Send(new CountReviewQuery()
            {
                ProductId = productId
            });

            return Ok(CustomAPIResponse<List<long>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteReview([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteReviewCommand()
            {
                Id = id
            });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpPatch("toggle/{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ToggleReview([FromRoute] long id)
        {
            var res = await _sender.Send(new ToggleReviewCommand()
            {
                Id = id
            });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteListReview([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListReviewCommand()
            {
                Ids = ids
            });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        // call from other service
        [InternalCommunicationAPI(APPLICATION_SERVICE.ORDER_SERVICE)]
        [HttpGet("internal/order-review")]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrderReview([FromQuery] GetOrderReviewRequest request)
        {
            var res = await _sender.Send(new GetOrderReviewQuery()
            {
                OrderItemIds = request.OrderItemIds,
                UserId = request.UserId
            });

            return Ok(CustomAPIResponse<OrderReviewDto>.Success(res, StatusCodes.Status200OK));
        }
    }
}