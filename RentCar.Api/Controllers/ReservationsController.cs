using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Contracts.Commands;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Application.Reports; 
using RentCar.Application.Reports.Queries;
using RentCar.Application.Reports.Queries.RentCar.Application.Reports.Queries;
using RentCar.Application.Reports.Rendering;
using RentCar.Domain.Authorization;
using System.Data;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        
         
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
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

        [HttpGet("business/{businessId:int}")]
        public async Task<ActionResult<List<ReservationDto>>> GetByBusiness(int businessId)
        {
            var result = await _mediator.Send(new GetReservationsByBusinessIdQuery(businessId));
            return Ok(result);
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

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteReservationCommand(id));

            if (!success)
                return NotFound();

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

        [HttpGet("check-availability")]
        public async Task<IActionResult> CheckAvailability([FromQuery] int carId,[FromQuery] DateTime from,[FromQuery] DateTime to,[FromQuery] int? excludeReservationId)
        {
            var available = await _mediator.Send(
                new CheckCarAvailabilityQuery(
                    carId,
                    from,
                    to,
                    excludeReservationId
                )
            );

            return Ok(new { available });
        }

        [HttpGet("{id:int}/contract-report")]
        public async Task<IActionResult> GenerateReservationContract(int id)
        {
            var pdf = await _mediator.Send(
                new GenerateReportQuery(
                    "RESERVATION_CONTRACT",
                    new Dictionary<string, object?>
                    {
                        ["ReservationId"] = id
                    }));

            return File(pdf, "application/pdf");
        }
    }
}
