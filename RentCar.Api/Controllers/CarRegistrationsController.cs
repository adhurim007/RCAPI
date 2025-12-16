using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.CarRegistration;
using RentCar.Application.Features.CarRegistration.Command;
using RentCar.Application.Features.CarRegistration.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/car-registrations")]
    public class CarRegistrationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarRegistrationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarRegistrationCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UpdateCarRegistrationCommand command)
        {
            var success = await _mediator.Send(command with { Id = id });
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteCarRegistrationCommand(id));
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("by-business/{businessId:int}")]
        public async Task<IActionResult> GetByBusiness(int businessId)
        {
            return Ok(await _mediator.Send(
                new GetCarRegistrationsByBusinessIdQuery(businessId)
            ));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(
                new GetCarRegistrationByIdQuery(id)
            ));
        }
    }
}
