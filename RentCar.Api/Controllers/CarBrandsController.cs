using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Domain.Authorization;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Application.Features.Cars.Queries.CarBrandAndModel;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarBrandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarBrandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/carbrands
        [HttpGet]
        [Authorize(Policy = Permissions.CarBrands.View)]
        public async Task<ActionResult<List<CarBrand>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarBrandsQuery());
            return Ok(result);
        }

        // GET: api/carbrands/{id}
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.CarBrands.View)]
        public async Task<ActionResult<CarBrand>> GetById(int id)
        {
            var brand = await _mediator.Send(new GetCarBrandByIdQuery(id));
            if (brand == null) return NotFound();
            return Ok(brand);
        }

        // POST: api/carbrands
        [HttpPost]
        [Authorize(Policy = Permissions.CarBrands.Create)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCarBrandCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/carbrands/{id}
        [HttpPut("{id}")]
        [Authorize(Policy = Permissions.CarBrands.Update)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCarBrandCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/carbrands/{id}
        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.CarBrands.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCarBrandCommand(id));
            return NoContent();
        }
    }
}
