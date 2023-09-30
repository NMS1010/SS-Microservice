using Amazon.Runtime.Internal;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Model.Paging;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Application.Model.Variant;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
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
        public async Task<IActionResult> GetProducts([FromQuery] GetProductPagingRequest request)
        {
            var query = _mapper.Map<GetAllProductQuery>(request);
            var res = await _sender.Send(query);
            return Ok(CustomAPIResponse<PaginatedResult<ProductDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("{productId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductById(string productId)
        {
            var res = await _sender.Send(new GetProductByIdQuery() { ProductId = productId });
            return Ok(CustomAPIResponse<ProductDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductRequest request)
        {
            var command = _mapper.Map<CreateProductCommand>(request);
            await _sender.Send(command);
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status201Created));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductRequest request)
        {
            var command = _mapper.Map<UpdateProductCommand>(request);
            var res = await _sender.Send(command);
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot update this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(string productId)
        {
            var res = await _sender.Send(new DeleteProductCommand() { ProductId = productId });
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot delete this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        // product image
        [HttpPost("images/add")]
        public async Task<IActionResult> CreateProductImage([FromForm] CreateProductImageRequest request)
        {
            var command = _mapper.Map<CreateProductImageCommand>(request);
            var res = await _sender.Send(command);
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot create image for this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpPut("images/update")]
        public async Task<IActionResult> UpdateProductImage([FromForm] UpdateProductImageRequest request)
        {
            var command = _mapper.Map<UpdateProductImageCommand>(request);
            var res = await _sender.Send(command);
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot update image for this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }

        [HttpDelete("images/delete/{productId}/{productImageId}")]
        public async Task<IActionResult> DeleteProductImage(string productId, string productImageId)
        {
            var res = await _sender.Send(new DeleteProductImageCommand() { ProductId = productId, ProductImageId = productImageId });
            if (!res)
                return Ok(CustomAPIResponse<NoContentAPIResponse>.Fail(StatusCodes.Status400BadRequest, "Cannot delete image for this product"));
            return Ok(CustomAPIResponse<NoContentAPIResponse>.Success(StatusCodes.Status204NoContent));
        }
    }
}