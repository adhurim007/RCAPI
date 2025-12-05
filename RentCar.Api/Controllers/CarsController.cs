using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Application.Features.Cars.Handlers;
using RentCar.Application.Features.Cars.Queries.CarBrandAndModel;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Authorization;

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
        //[Authorize(Policy = Permissions.Cars.Create)]
        //[Authorize(Roles = "Business")]  
        public async Task<IActionResult> Create([FromBody] CreateCarCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCarCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var result = await _mediator.Send(command);

            if (!result)
                return NotFound("Car not found");

            return NoContent();
        }
         
        [HttpGet]
        [AllowAnonymous]
        [Authorize(Policy = Permissions.Cars.View)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]
        //[Authorize(Policy = Permissions.Cars.View)]
        public async Task<IActionResult> GetById(int id)
        {
            var car = await _mediator.Send(new GetCarByIdQuery(id));
            return Ok(car);
        }

        //[HttpPut("images/{imageId}")]
        //[Authorize(Roles = "Business,Admin")] 
        //[Authorize(Policy = Permissions.Cars.Update)]
        //public async Task<IActionResult> UpdateImage(int imageId, [FromForm] IFormFile file)
        //{
        //    if (file == null)
        //        return BadRequest("Image file is required.");

        //    var result = await _mediator.Send(new UpdateCarImageCommand(imageId, file));
        //    return Ok(result);
        //}


        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Cars.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCarCommand { Id = id });

            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("by-business/{businessId}")]
        public async Task<IActionResult> getByBusiness(int businessId)
        {
            var cars = await _mediator.Send(new GetCarsByBusinessIdQuery { BusinessId = businessId });
            return Ok(cars);
        }

        [HttpPut("{id}/availability")]
        [Authorize(Policy = Permissions.Cars.Update)]
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
        [Authorize(Policy = Permissions.Cars.ManageImages)]
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
        [Authorize(Policy = Permissions.Cars.ManageImages)]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var result = await _mediator.Send(new DeleteCarImageCommand(imageId));
            if (!result)
                return NotFound("Image not found.");

            return NoContent();
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<LookupDto>>> GetBrands()
         => Ok(await _mediator.Send(new GetCarBrandsQuery()));

        [HttpGet("models/{brandId}")]
        public async Task<ActionResult<List<LookupDto>>> GetModels(int brandId)
         => Ok(await _mediator.Send(new GetCarModelsByBrandQuery(brandId)));

        [HttpGet("cartypes")]
        public async Task<ActionResult<List<LookupDto>>> GetCarTypes()
            => Ok(await _mediator.Send(new GetCarTypesQuery()));

        [HttpGet("fueltypes")]
        public async Task<ActionResult<List<LookupDto>>> GetFuelTypes()
            => Ok(await _mediator.Send(new GetFuelTypesQuery()));

        [HttpGet("transmissions")]
        public async Task<ActionResult<List<LookupDto>>> GetTransmissions()
            => Ok(await _mediator.Send(new GetTransmissionsQuery()));



    }
}
