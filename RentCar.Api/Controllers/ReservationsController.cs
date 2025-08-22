using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Authorization;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Contracts.Commands;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Application.Reports;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ReservationReportService _reportService;

        public ReservationsController(IMediator mediator, ReservationReportService reportService)
        {
            _mediator = mediator;
            _reportService = reportService;
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Reservations.Create)]
        public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
        {
            var reservationId = await _mediator.Send(command);
            return Ok(new { ReservationId = reservationId });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetReservationByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("client/{clientId}")]
        public async Task<ActionResult<List<ReservationDto>>> GetByClient(int clientId)
        {
            return Ok(await _mediator.Send(new GetReservationsByClientIdQuery(clientId)));
        }

        [HttpGet("business/{businessId}")]
        public async Task<ActionResult<List<ReservationDto>>> GetByBusiness(int businessId)
        {
            return Ok(await _mediator.Send(new GetReservationsByBusinessIdQuery(businessId)));
        }

        [HttpGet]
        public async Task<ActionResult<List<ReservationDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllReservationsQuery()));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateReservationCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");

            var success = await _mediator.Send(command);
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelReservation(int id, [FromBody] string reason)
        {
            var result = await _mediator.Send(new CancelReservationCommand(id, reason));
            return result ? Ok("Reservation canceled.") : BadRequest("Unable to cancel reservation.");
        }

        [HttpPut("{id}/approve")]
        [Authorize(Policy = Permissions.Reservations.Approve)]
        public async Task<IActionResult> Approve(int id, [FromQuery] int approvedBy)
        {
            var success = await _mediator.Send(new ApproveReservationCommand(id, approvedBy));
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpPut("{id}/reject")]
        public async Task<IActionResult> Reject(int id, [FromBody] string reason)
        {
            var success = await _mediator.Send(new RejectReservationCommand(id, reason));
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpGet("{reservationId}/history")]
        public async Task<IActionResult> GetReservationHistory(int reservationId)
        {
            var result = await _mediator.Send(new GetReservationHistoryQuery(reservationId));
            return Ok(result);
        }

        [HttpPost("{reservationId}/history")]
        public async Task<IActionResult> AddReservationHistory(int reservationId, [FromBody] AddReservationHistoryCommand command)
        {
            if (reservationId != command.ReservationId) return BadRequest("ReservationId mismatch");

            var id = await _mediator.Send(command);
            return Ok(new { HistoryId = id });
        }

        [HttpPost("{reservationId}/generate-contract")]
        public async Task<IActionResult> GenerateContract(int reservationId)
        {
            var contractId = await _mediator.Send(new GenerateContractCommand(reservationId));
            return Ok(new { ContractId = contractId });
        }

        [HttpGet("report/list")]
        [Authorize(Policy = AuthorizationPolicies.ViewReports)]
        public async Task<IActionResult> GetReservationListReport(DateTime from, DateTime to, int? businessId)
        {
            var pdf = await _reportService.GenerateReservationListReport(from, to, businessId);
            return File(pdf, "application/pdf", "ReservationList.pdf");
        }

        [HttpGet("report/income")]
        [Authorize(Policy = AuthorizationPolicies.ViewReports)]
        public async Task<IActionResult> GetIncomeReport(DateTime from, DateTime to)
        {
            var pdf = await _reportService.GenerateIncomeReport(from, to);
            return File(pdf, "application/pdf", "IncomeReport.pdf");
        }

        [HttpGet("report/pending")]
        [Authorize(Policy = AuthorizationPolicies.ViewReports)]
        public async Task<IActionResult> GetPendingReservationsReport()
        {
            var pdf = await _reportService.GeneratePendingReservationsReport();
            return File(pdf, "application/pdf", "PendingReservations.pdf");
        }


    }
}
