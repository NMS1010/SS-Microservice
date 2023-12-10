using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Common.Types.Model.Paging;
using SS_Microservice.Services.Products.Application.Dto;
using SS_Microservice.Services.Products.Application.Features.Unit.Command;
using SS_Microservice.Services.Products.Application.Features.Unit.Query;
using SS_Microservice.Services.Products.Application.Model.Unit;

namespace SS_Microservice.Services.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class UnitsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public UnitsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetListUnit([FromQuery] GetUnitPagingRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetListUnitQuery>(request));

            return Ok(new CustomAPIResponse<PaginatedResult<UnitDto>>(res, StatusCodes.Status200OK));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUnit([FromRoute] long id)
        {
            var res = await _sender.Send(new GetUnitQuery() { Id = id });

            return Ok(new CustomAPIResponse<UnitDto>(res, StatusCodes.Status200OK));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUnit([FromForm] CreateUnitRequest request)
        {
            var unitId = await _sender.Send(_mapper.Map<CreateUnitCommand>(request));
            var url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/units/{unitId}";

            return Created(url, new CustomAPIResponse<object>(new { id = unitId }, StatusCodes.Status201Created));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit([FromRoute] long id, [FromForm] UpdateUnitRequest request)
        {
            request.Id = id;
            var res = await _sender.Send(_mapper.Map<UpdateUnitCommand>(request));

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnit([FromRoute] long id)
        {
            var res = await _sender.Send(new DeleteUnitCommand() { Id = id });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteListUnit([FromQuery] List<long> ids)
        {
            var res = await _sender.Send(new DeleteListUnitCommand() { Ids = ids });

            return Ok(new CustomAPIResponse<bool>(res, StatusCodes.Status204NoContent));
        }
    }
}