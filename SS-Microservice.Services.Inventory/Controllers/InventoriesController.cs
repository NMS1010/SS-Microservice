using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Services.Auth.Application.Model.CustomResponse;
using SS_Microservice.Services.Inventory.Application.Dto;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Commands;
using SS_Microservice.Services.Inventory.Application.Features.Docket.Queries;
using SS_Microservice.Services.Inventory.Application.Models.Inventory;

namespace SS_Microservice.Services.Inventory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN, STAFF")]
    public class InventoriesController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public InventoriesController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("{productId}")]
        public async Task<ActionResult> GetListDocketByProduct([FromRoute] long productId)
        {
            var res = await _sender.Send(new GetListDocketQuery()
            {
                ProductId = productId
            });

            return Ok(CustomAPIResponse<List<DocketDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<ActionResult> ImportProduct([FromBody] ImportProductRequest request)
        {
            var res = await _sender.Send(_mapper.Map<ImportProductCommand>(request));

            return Ok(CustomAPIResponse<bool>.Success(res, StatusCodes.Status204NoContent));
        }
    }
}