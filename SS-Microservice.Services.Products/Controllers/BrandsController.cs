using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
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
        private ISender _sender;
        private IMapper _mapper;

        public BrandsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrands([FromQuery] GetBrandPagingRequest request)
        {
            var query = _mapper.Map<GetAllBrandQuery>(request);
            var res = await _sender.Send(query);
            return Ok(CustomAPIResponse<PaginatedResult<BrandDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrandById([FromRoute] string brandId)
        {
            var res = await _sender.Send(new GetBrandByIdQuery() { Id = brandId });
            return Ok(CustomAPIResponse<BrandDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddBrand([FromForm] CreateBrandRequest request)
        {
            var command = _mapper.Map<CreateBrandCommand>(request);
            var isSuccess = await _sender.Send(command);
            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateBrand([FromForm] UpdateBrandRequest request)
        {
            var command = _mapper.Map<UpdateBrandCommand>(request);
            var isSuccess = await _sender.Send(command);
            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{brandId}")]
        public async Task<IActionResult> DeleteBrand(string brandId)
        {
            var isSuccess = await _sender.Send(new DeleteBrandCommand() { Id = brandId });

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }
    }
}