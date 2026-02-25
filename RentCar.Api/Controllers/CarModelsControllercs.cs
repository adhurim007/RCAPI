using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.CarModel;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Application.Features.Cars.Queries.CarBrandAndModel;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Authorization;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CarModelsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarModelsController(IMediator mediator)
        {
            _mediator = mediator;
        }
         
        [HttpGet]
        //[Authorize(Policy = Permissions.CarModels.View)]
        public async Task<ActionResult<List<CarModelDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarModelsQuery());
            return Ok(result);
        }

        // GET: api/carmodels/{id}
        [HttpGet("{id}")]
       // [Authorize(Policy = Permissions.CarModels.View)]
        public async Task<ActionResult<CarModelDto>> GetById(int id)
        {
            var model = await _mediator.Send(new GetCarModelByIdQuery(id));
            if (model == null) return NotFound();
            return Ok(model);
        }

        // POST: api/carmodels
        [HttpPost]
        //[Authorize(Policy = Permissions.CarModels.Create)]
        public async Task<ActionResult<int>> Create([FromBody] CreateCarModelCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        // PUT: api/carmodels/{id}
        [HttpPut("{id}")]
        //[Authorize(Policy = Permissions.CarModels.Update)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCarModelCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/carmodels/{id}
        [HttpDelete("{id}")]
        //[Authorize(Policy = Permissions.CarModels.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCarModelCommand(id));
            return NoContent();
        }
    }
}
