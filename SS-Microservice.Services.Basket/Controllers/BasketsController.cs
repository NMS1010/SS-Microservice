using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Basket.Application.Dto;
using SS_Microservice.Services.Basket.Application.Features.Basket.Queries;
using SS_Microservice.Services.Basket.Application.Features.Basket.Commands;
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

        [HttpGet("all")]
        public async Task<IActionResult> GetBasket([FromQuery] BasketPagingRequest request)
        {
            var query = _mapper.Map<GetBasketQuery>(request);
            query.UserId = _currentUserService.UserId;
            var basket = await _sender.Send(query);

            return Ok(CustomAPIResponse<BasketDto>.Success(basket, StatusCodes.Status200OK));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProductToBasket([FromBody] BasketAddRequest request)
        {
            var command = new AddBasketCommand()
            {
                UserId = _currentUserService.UserId,
                ProductId = request.ProductId,
                Quantity = request.Quantity,
            };
            await _sender.Send(command);

            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpDelete("delete/{basketItemId}")]
        public async Task<IActionResult> RemoveProductFromBasket(int basketItemId)
        {
            var command = new DeleteBasketCommand()
            {
                UserId = _currentUserService.UserId,
                BasketItemId = basketItemId
            };
            var isSuccess = await _sender.Send(command);
            if (isSuccess)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Failed to remove this product"));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProductQuantity([FromBody] BasketUpdateRequest request)
        {
            var command = new UpdateBasketCommand()
            {
                UserId = _currentUserService.UserId,
                BasketItemId = request.BasketItemId,
                Quantity = request.Quantity,
            };
            var isSuccess = await _sender.Send(command);

            if (isSuccess)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Failed to update quantity for this product"));
        }
    }
}