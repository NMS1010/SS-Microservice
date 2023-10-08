using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Variant.Commands;
using SS_Microservice.Services.Products.Application.Features.Variant.Queries;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class VariantsController : ControllerBase
    {
        private ISender _sender;
        private IMapper _mapper;

        public VariantsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetVariants([FromQuery] GetVariantPagingRequest request)
        {
            var query = _mapper.Map<GetAllVariantQuery>(request);
            var res = await _sender.Send(query);
            return Ok(CustomAPIResponse<PaginatedResult<VariantDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{productId}/{variantId}")]
        public async Task<IActionResult> GetVariantById([FromRoute] string variantId, [FromRoute] string productId)
        {
            var res = await _sender.Send(new GetVariantByIdQuery() { ProductId = productId, VariantId = variantId });
            return Ok(CustomAPIResponse<VariantDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddVariant([FromForm] CreateVariantRequest request)
        {
            var command = _mapper.Map<CreateVariantCommand>(request);
            var isSuccess = await _sender.Send(command);
            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateVariant([FromForm] UpdateVariantRequest request)
        {
            var command = _mapper.Map<UpdateVariantCommand>(request);
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{productId}/{variantId}")]
        public async Task<IActionResult> DeleteVariant([FromRoute] string variantId, [FromRoute] string productId)
        {
            var isSuccess = await _sender.Send(new DeleteVariantCommand() { ProductId = productId, VariantId = variantId });

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }
    }
}