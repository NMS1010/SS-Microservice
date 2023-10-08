using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Category.Commands;

using SS_Microservice.Services.Products.Application.Features.Category.Queries;
using SS_Microservice.Services.Products.Application.Model.Category;

namespace SS_Microservice.Services.Categories.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class CategoriesController : ControllerBase
    {
        private ISender _sender;
        private IMapper _mapper;

        public CategoriesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategories([FromQuery] GetCategoryPagingRequest request)
        {
            var query = _mapper.Map<GetAllCategoryQuery>(request);
            var res = await _sender.Send(query);
            return Ok(CustomAPIResponse<PaginatedResult<CategoryDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] string categoryId)
        {
            var res = await _sender.Send(new GetCategoryByIdQuery() { Id = categoryId });
            return Ok(CustomAPIResponse<CategoryDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCategory([FromForm] CreateCategoryRequest request)
        {
            var command = _mapper.Map<CreateCategoryCommand>(request);
            var isSuccess = await _sender.Send(command);
            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCategory([FromForm] UpdateCategoryRequest request)
        {
            var command = _mapper.Map<UpdateCategoryCommand>(request);
            var isSuccess = await _sender.Send(command);

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{categoryId}")]
        public async Task<IActionResult> DeleteCategory(string categoryId)
        {
            var isSuccess = await _sender.Send(new DeleteCategoryCommand() { Id = categoryId });

            return Ok(CustomAPIResponse<bool>.Success(isSuccess, StatusCodes.Status204NoContent));
        }
    }
}