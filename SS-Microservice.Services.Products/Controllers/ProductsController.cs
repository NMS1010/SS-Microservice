using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Attributes;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Product.Commands;
using SS_Microservice.Services.Products.Application.Features.Product.Queries;
using SS_Microservice.Services.Products.Application.Features.ProductImage.Commands;
using SS_Microservice.Services.Products.Application.Features.ProductImage.Queries;
using SS_Microservice.Services.Products.Application.Model.Product;
using SS_Microservice.Services.Products.Application.Model.ProductImage;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class ProductsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public ProductsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetListProduct([FromQuery] GetProductPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListProductQuery>(request));

            return Ok(new CustomAPIResponse<PaginatedResult<ProductDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListFilteringProduct([FromQuery] FilterProductPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListFilteringProductQuery>(request));

            return Ok(new CustomAPIResponse<PaginatedResult<ProductDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListSearchingProduct([FromQuery] SearchProductPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListSearchingProductQuery>(request));

            return Ok(new CustomAPIResponse<PaginatedResult<ProductDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] long id)
        {
            var res = await _sender.Send(new GetProductQuery() { Id = id });

            return Ok(new CustomAPIResponse<ProductDto>(res, StatusCodes.Status200OK));
        }

        [HttpGet("detail/{slug}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductBySlug([FromRoute] string slug)
        {
            var res = await _sender.Send(new GetProductBySlugQuery() { Slug = slug });

            return Ok(new CustomAPIResponse<ProductDto>(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromForm] CreateProductRequest request)
        {
            var productId = await _sender.Send(_mapper.Map<CreateProductCommand>(request));
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/products/{productId}";

            return Created(url, new CustomAPIResponse<object>(new { id = productId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] long id, [FromForm] UpdateProductRequest request)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<UpdateProductCommand>(request));

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteProductCommand() { Id = id });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListProduct([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListProductCommand() { Ids = ids });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        // product images api

        [HttpGet("images")]
        public async Task<IActionResult> GetListProductImage([FromQuery] long productId)
        {
            var res = await _sender.Send(new GetListProductImageQuery() { ProductId = productId });

            return Ok(new CustomAPIResponse<List<ProductImageDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("images/{id}")]
        public async Task<IActionResult> GetProductImage([FromRoute] long id)
        {
            var res = await _sender.Send(new GetProductImageQuery() { Id = id });

            return Ok(new CustomAPIResponse<ProductImageDto>(res, StatusCodes.Status200OK));
        }

        [HttpPost("images")]
        public async Task<IActionResult> CreateProductImage([FromForm] CreateProductImageRequest request)
        {
            var resp = await _sender.Send(_mapper.Map<CreateProductImageCommand>(request));
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/products/images";

            return Created(url, new CustomAPIResponse<object>(new { id = resp }, StatusCodes.Status201Created));
        }

        [HttpPut("images/{id}")]
        public async Task<IActionResult> UpdateProductImage([FromRoute] long id, [FromForm] UpdateProductImageRequest request)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<UpdateProductImageCommand>(request));

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpPatch("images/{id}")]
        public async Task<IActionResult> SetDefaultProductImage([FromRoute] long id, [FromForm] long productId)
        {
            var res = await _sender.Send(new SetDefaultProductImageCommand() { Id = id, ProductId = productId });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("images/{id}")]
        public async Task<IActionResult> DeleteProductImage([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteProductImageCommand() { Id = id });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("images")]
        public async Task<IActionResult> DeleteListProductImage([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListProductImageCommand() { Ids = ids });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }


        // call from other service
        [InternalCommunicationAPI(APPLICATION_SERVICE.USER_OPERATION_SERVICE)]
        [HttpGet("internal/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProductFromOtherService([FromRoute] long id)
        {
            var res = await _sender.Send(new GetProductQuery() { Id = id });

            return Ok(new CustomAPIResponse<ProductDto>(res, StatusCodes.Status200OK));
        }
    }
}