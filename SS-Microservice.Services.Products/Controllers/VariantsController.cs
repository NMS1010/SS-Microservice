using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public VariantsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListVariant([FromQuery] long productId)
        {
            var res = await _sender.Send(new GetListVariantByProductQuery() { ProductId = productId });

            return Ok(new CustomAPIResponse<List<VariantDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVariant([FromRoute] long id)
        {
            var res = await _sender.Send(new GetVariantQuery() { Id = id });

            return Ok(new CustomAPIResponse<VariantDto>(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateVariant([FromBody] CreateVariantRequest request)
        {
            var variantId = await _sender.Send(_mapper.Map<CreateVariantCommand>(request));
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/variants/{variantId}";

            return Created(url, new CustomAPIResponse<object>(new { id = variantId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVariant([FromRoute] long id, [FromBody] UpdateVariantRequest request)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<UpdateVariantCommand>(request));

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVariant([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteVariantCommand() { Id = id });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListVariant([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListVariantCommand() { Ids = ids });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }
    }
}