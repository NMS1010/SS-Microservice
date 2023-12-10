using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Brand.Commands;
using SS_Microservice.Services.Products.Application.Features.Brand.Queries;
using SS_Microservice.Services.Products.Application.Model.Brand;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class BrandsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public BrandsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListBrand([FromQuery] GetBrandPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListBrandQuery>(request));

            return Ok(new CustomAPIResponse<PaginatedResult<BrandDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBrand([FromRoute] long id)
        {
            var res = await _sender.Send(new GetBrandQuery() { Id = id });

            return Ok(new CustomAPIResponse<BrandDto>(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromForm] CreateBrandRequest request)
        {
            var brandId = await _sender.Send(_mapper.Map<CreateBrandCommand>(request));
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/brands/{brandId}";

            return Created(url, new CustomAPIResponse<object>(new { id = brandId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBrand([FromRoute] long id, [FromForm] UpdateBrandRequest request)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<UpdateBrandCommand>(request));

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteBrandCommand() { Id = id });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListBrand([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListBrandCommand() { Ids = ids });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }
    }
}