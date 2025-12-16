using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.CarService.Commands;
using RentCar.Application.Features.CarService.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/car-services")]
    public class CarServicesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarServicesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCarServiceCommand command)
        {
            var id = await _mediator.Send(command);
            return Ok(id);
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] UpdateCarServiceCommand command
        )
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var success = await _mediator.Send(command);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteCarServiceCommand(id));
            if (!success) return NotFound();
            return NoContent();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetCarServiceByIdQuery(id));
            if (result == null) return NotFound();
            return Ok(result);
        }

        // LIST BY CAR
        [HttpGet("by-car/{carId:int}")]
        public async Task<IActionResult> GetByCar(int carId)
        {
            return Ok(await _mediator.Send(new GetCarServicesByCarIdQuery(carId)));
        }

        // LIST BY BUSINESS
        [HttpGet("by-business/{businessId:int}")]
        public async Task<IActionResult> GetByBusiness(int businessId)
        {
            return Ok(await _mediator.Send(
                new GetCarServicesByBusinessIdQuery(businessId)
            ));
        }
    }
}
