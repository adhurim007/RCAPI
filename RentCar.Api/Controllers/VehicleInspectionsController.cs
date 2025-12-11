using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs.VehicleInspection;
using RentCar.Application.Features.VehicleInspection.Commands;
using RentCar.Application.Features.VehicleInspection.Queries;
using RentCar.Domain.Authorization;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleInspectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehicleInspectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        //[Authorize(Policy = Permissions.Reservations.View)] // admin
        public async Task<ActionResult<List<VehicleInspectionDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllVehicleInspectionsQuery());
            return Ok(result);
        }

        [HttpGet("reservation/{reservationId}")]
        public async Task<IActionResult> GetByReservation(int reservationId)
            => Ok(await _mediator.Send(new GetInspectionsByReservationQuery(reservationId)));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _mediator.Send(new GetInspectionByIdQuery(id)));

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreateInspectionCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id:int}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update( int id,
        [FromForm] UpdateInspectionCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }
         
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeleteInspectionCommand(id)));
    }
}
