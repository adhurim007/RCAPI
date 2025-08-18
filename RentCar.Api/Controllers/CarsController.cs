using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Application.Features.Cars.Handlers;
using RentCar.Application.Features.Cars.Queries.GetAllCars;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Business")]  
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
        }
          
        [HttpGet]
        [AllowAnonymous]  
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var car = await _mediator.Send(new GetCarByIdQuery(id));
            return Ok(car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCarCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteCarCommand { Id = id });

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-business/{businessId}")]
        public async Task<IActionResult> GetByBusinessId(int businessId)
        {
            var cars = await _mediator.Send(new GetCarsByBusinessIdQuery { BusinessId = businessId });
            return Ok(cars);
        }

        [HttpPut("{id}/availability")]
        public async Task<IActionResult> SetAvailability(Guid id, [FromBody] bool isAvailable)
        {
            var result = await _mediator.Send(new SetCarAvailabilityCommand
            {
                CarId = id,
                IsAvailable = isAvailable
            });

            if (!result)
                return NotFound("Car not found.");

            return NoContent();
        }

    }
}
