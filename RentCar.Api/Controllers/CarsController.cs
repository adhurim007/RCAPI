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
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _mediator.Send(new GetCarByIdQuery(id));
            return Ok(car);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCarCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
