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

        [HttpPut("images/{imageId}")]
        [Authorize(Roles = "Business,Admin")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] IFormFile file)
        {
            if (file == null)
                return BadRequest("Image file is required.");

            var result = await _mediator.Send(new UpdateCarImageCommand(imageId, file));
            return Ok(result);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
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
        public async Task<IActionResult> SetAvailability(int id, [FromBody] bool isAvailable)
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

        [HttpPost("{carId}/images")]
        [Authorize(Roles = "Business")]
        public async Task<IActionResult> UploadImages(int carId, [FromForm] List<IFormFile> files)
        {
            var result = await _mediator.Send(new UploadCarImageCommand
            {
                CarId = carId,
                Files = files
            });

            return Ok(result);
        }

        [HttpDelete("images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var result = await _mediator.Send(new DeleteCarImageCommand(imageId));
            if (!result)
                return NotFound("Image not found.");

            return NoContent();
        }


    }
}
