using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Application.Product.Commands;
using SS_Microservice.Services.Products.Application.Product.Queries;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private ISender _sender;
        private IMapper _mapper;

        public ProductsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("all")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProducts([FromQuery] ProductPagingRequest request)
        {
            var query = _mapper.Map<ProductGetAllQuery>(request);
            var res = await _sender.Send(query);
            return Ok(CustomAPIResponse<PaginatedResult<ProductDTO>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(string productId)
        {
            var res = await _sender.Send(new ProductGetByIdQuery() { ProductId = productId });
            return Ok(CustomAPIResponse<ProductDTO>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddProduct([FromForm] ProductCreateRequest request)
        {
            var command = _mapper.Map<ProductCreateCommand>(request);
            await _sender.Send(command);
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductUpdateRequest request)
        {
            var command = _mapper.Map<ProductUpdateCommand>(request);
            var res = await _sender.Send(command);
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot update this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpPut("update/images")]
        public async Task<IActionResult> UpdateProductImage([FromForm] ProductImageUpdateRequest request)
        {
            var command = _mapper.Map<ProductImageUpdateCommand>(request);
            var res = await _sender.Send(command);
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot update images for this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            var res = await _sender.Send(new ProductDeleteCommand() { ProductId = productId });
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot delete this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/images/{productId}/{productImageId}")]
        public async Task<IActionResult> DeleteProductImage(string productId, string productImageId)
        {
            var res = await _sender.Send(new ProductImageDeleteCommand() { ProductId = productId, ProductImageId = productImageId });
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot delete image for this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }
    }
}