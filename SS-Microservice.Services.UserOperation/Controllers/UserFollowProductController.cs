using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Commands;
using SS_Microservice.Services.UserOperation.Application.Features.UserFollowProduct.Queries;
using SS_Microservice.Services.UserOperation.Application.Models.UserFollowProduct;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product.Model.Response;

namespace SS_Microservice.Services.UserOperation.Controllers
{
    [Route("api/user-follow-products")]
    [ApiController]
    [Authorize]
    public class UserFollowProductController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UserFollowProductController(ICurrentUserService currentUserService, ISender sender, IMapper mapper)
        {
            _currentUserService = currentUserService;
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost("like")]
        public async Task<ActionResult> FollowProduct([FromBody] FollowProductRequest request)
        {
            request.UserId = _currentUserService.UserId;
            await _sender.Send(_mapper.Map<FollowProductCommand>(request));

            return Created("", CustomAPIResponse<object>.Success(new { id = request.ProductId }, StatusCodes.Status201Created));
        }

        [HttpPost("unlike")]
        public async Task<ActionResult> UnFollowProduct([FromBody] UnFollowProductRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var res = await _sender.Send(_mapper.Map<UnFollowProductCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpGet]
        public async Task<ActionResult> GetListFollowProduct([FromQuery] GetFollowProductPagingRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var resp = await _sender.Send(_mapper.Map<GetListFollowProductQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<ProductDto>>.Success(resp, StatusCodes.Status200OK));
        }
    }
}