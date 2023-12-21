using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.UserOperation.Application.Dto;
using SS_Microservice.Services.UserOperation.Application.Features.Statistic.Queries;
using SS_Microservice.Services.UserOperation.Application.Models.Statistic;

namespace SS_Microservice.Services.UserOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class StatisticsController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly IMapper _mapper;

        public StatisticsController(ISender sender, IMapper mapper)
        {
            _sender = sender;
            _mapper = mapper;
        }

        [HttpGet("rating")]
        public async Task<IActionResult> GetStatisticReview([FromQuery] GetStatisticReviewRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetStatisticReviewQuery>(request));

            return Ok(CustomAPIResponse<List<StatisticReviewDto>>.Success(res, StatusCodes.Status200OK));
        }
    }
}
