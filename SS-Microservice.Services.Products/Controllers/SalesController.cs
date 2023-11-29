using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Sale.Commands;
using SS_Microservice.Services.Products.Application.Features.Sale.Queries;
using SS_Microservice.Services.Products.Application.Model.Sale;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class SalesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public SalesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListSale([FromQuery] GetSalePagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListSaleQuery>(request));

            return Ok(new CustomAPIResponse<PaginatedResult<SaleDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("latest")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSaleLatest()
        {
            var res = await _sender.Send(new GetLatestSaleQuery());

            return Ok(new CustomAPIResponse<SaleDto>(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetSale([FromRoute] long id)
        {
            var res = await _sender.Send(new GetSaleQuery() { Id = id });

            return Ok(new CustomAPIResponse<SaleDto>(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateSale([FromForm] CreateSaleRequest request)
        {
            var saleId = await _sender.Send(_mapper.Map<CreateSaleCommand>(request));
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/sales/{saleId}";

            return Created(url, new CustomAPIResponse<object>(new { id = saleId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSale([FromRoute] long id, [FromForm] UpdateSaleRequest request)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<UpdateSaleCommand>(request));

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteSaleCommand() { Id = id });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListSale([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListSaleCommand() { Ids = ids });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpPost("apply")]
        public async Task<IActionResult> ApplySale([FromForm] long id)
        {
            var res = await _sender.Send(new ApplySaleCommand() { Id = id });
            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpPost("cancel")]
        public async Task<IActionResult> CancelSale([FromForm] long id)
        {
            var res = await _sender.Send(new CancelSaleCommand() { Id = id });
            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }
    }
}