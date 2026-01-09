using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.VehicleDamage.Command;
using RentCar.Application.Features.VehicleDamage.Queries;
using RentCar.Application.Reports.Queries.RentCar.Application.Reports.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleDamagesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleDamagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetVehicleDamageByIdQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // LIST
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int? reservationId)
        {
            var result = await _mediator.Send(
                new GetVehicleDamagesQuery(reservationId));
            return Ok(result);
        }

        // CREATE
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(
            [FromForm] CreateVehicleDamageCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        // UPDATE
        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] UpdateVehicleDamageCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var success = await _mediator.Send(command);
            return success ? NoContent() : NotFound();
        }

        // DELETE
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(
                new DeleteVehicleDamageCommand(id));

            return success ? NoContent() : NotFound();
        }

        [HttpGet("{id:int}/damage-report")]
        public async Task<IActionResult> GenerateDamageInspectionReport(int id)
        {
            var pdf = await _mediator.Send(
                new GenerateReportQuery(
                    "VEHICLE_DAMAGE_REPORT",
                    new Dictionary<string, object?>
                    {
                        ["ReservationId"] = id
                    }));

            return File(pdf, "application/pdf");
        }
    }

}
