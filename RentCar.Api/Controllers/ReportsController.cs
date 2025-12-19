using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Reports.Core;
using RentCar.Application.Reports.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Kthen listën e raporteve të disponueshme
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAvailableReports()
        {
            var reports = await _mediator.Send(new GetAvailableReportsQuery());
            return Ok(reports);
        }

        /// <summary>
        /// Gjeneron raport bazuar në ReportCode dhe parametra
        /// </summary>
        [HttpPost("generate")]
        public async Task<IActionResult> GenerateReport([FromBody] ReportRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.ReportCode))
                return BadRequest("ReportCode is required.");

            var result = await _mediator.Send(new GenerateReportQuery(request));
            return Ok(result);
        }
    }
}
