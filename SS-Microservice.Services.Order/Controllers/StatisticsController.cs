using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SS_Microservice.Common.Types.Model.CustomResponse;
using SS_Microservice.Services.Order.Application.Dtos;
using SS_Microservice.Services.Order.Application.Features.Statistic.Queries;
using SS_Microservice.Services.Order.Application.Models.Statistic;

namespace SS_Microservice.Services.Order.Controllers
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

        [HttpGet("total")]
        public async Task<IActionResult> GetStatisticTotal()
        {
            var res = await _sender.Send(new GetStatisticTotalQuery());

            return Ok(CustomAPIResponse<StatisticTotalDto>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("revenue")]
        public async Task<IActionResult> GetStatisticRevenue([FromQuery] GetStatisticRevenueRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetStatisticRevenueQuery>(request));

            return Ok(CustomAPIResponse<List<StatisticRevenueDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("order-status")]
        public async Task<IActionResult> GetStatisticOrderStatus([FromQuery] GetStatisticOrderStatusRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetStatisticOrderStatusQuery>(request));

            return Ok(CustomAPIResponse<List<StatisticOrderStatusDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("top-selling-product-year")]
        public async Task<IActionResult> StatisticTopSellingProductYear([FromQuery] GetStatisticTopSellingProductYearRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetStatisticTopSellingProductYearQuery>(request));

            return Ok(CustomAPIResponse<List<StatisticTopSellingProductYearDto>>.Success(res, StatusCodes.Status200OK));
        }

        [HttpGet("top-selling-product")]
        public async Task<IActionResult> StatisticTopSellingProductYear([FromQuery] GetStatisticTopSellingProductRequest request)
        {
            var res = await _sender.Send(_mapper.Map<GetStatisticTopSellingProductQuery>(request));

            return Ok(CustomAPIResponse<List<StatisticTopSellingProductDto>>.Success(res, StatusCodes.Status200OK));
        }

    }
}
