using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Model;

namespace SS_Microservice.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BasketsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public BasketsController(ISender sender, ICurrentUserService currentUserService, IMapper mapper)
        {
            _sender = sender;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddVariantItemToBasket([FromBody] CreateBasketItemRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var res = await _sender.Send(_mapper.Map<CreateBasketItemCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateBasketItemQuantity([FromRoute] long cartItemId, [FromBody] UpdateBasketItemRequest request)
        {
            request.CartItemId = cartItemId;
            request.UserId = _currentUserService.UserId;

            var res = await _sender.Send(_mapper.Map<UpdateBasketItemCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> DeleteBasketItem([FromRoute] long cartItemId)
        {
            var res = await _sender.Send(new DeleteBasketItemCommand()
            {
                CartItemId = cartItemId,
                UserId = _currentUserService.UserId
            });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListBasketItem([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListBasketItemCommand()
            {
                Ids = ids,
                UserId = _currentUserService.UserId
            });

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketByUser([FromQuery] GetBasketPagingRequest request)
        {
            request.UserId = _currentUserService.UserId;
            var res = await _sender.Send(_mapper.Map<GetListBasketByUserQuery>(request));

            return Ok(CustomAPIResponse<PaginatedResult<BasketItemDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("list-ids")]
        public async Task<IActionResult> GetBasketItemByIds([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new GetListBasketItemQuery()
            {
                Ids = ids,
                UserId = _currentUserService.UserId
            });

            return Ok(CustomAPIResponse<List<BasketItemDto>>.Success(res, StatusCodes.Status200OK));
        }
    }
}