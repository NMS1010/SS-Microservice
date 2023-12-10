using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;
using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Features.ListCategory.Commands;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Categories.Controllers
{
    [Route("api/product-categories")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class CategoriesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public CategoriesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListCategory([FromQuery] GetCategoryPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListCategoryQuery>(request));

            return Ok(new CustomAPIResponse<PaginatedResult<CategoryDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory([FromRoute] long id)
        {
            var res = await _sender.Send(new GetCategoryQuery() { Id = id });

            return Ok(new CustomAPIResponse<CategoryDto>(res, StatusCodes.Status200OK));
        }

        [HttpGet("slug/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryBySlug([FromRoute] string slug)
        {
            var res = await _sender.Send(new GetCategoryBySlugQuery() { Slug = slug });

            return Ok(new CustomAPIResponse<CategoryDto>(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequest request)
        {
            var productCategoryId = await _sender.Send(_mapper.Map<CreateCategoryCommand>(request));
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/product-categories/{productCategoryId}";

            return Created(url, new CustomAPIResponse<object>(new { id = productCategoryId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] long id, [FromForm] UpdateCategoryRequest request)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<UpdateCategoryCommand>(request));

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteCategoryCommand() { Id = id });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListCategory([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListCategoryCommand() { Ids = ids });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }
    }
}