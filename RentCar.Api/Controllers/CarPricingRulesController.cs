using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Domain.Authorization;
using RentCar.Application.Features.CarPricingRules.Command;
using RentCar.Application.Features.CarPricingRules.Queries;
using RentCar.Application.Features.Cars.Queries.GetAllCars;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarPricingRulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarPricingRulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        //[Authorize(Policy = Permissions.CarPricingRules.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCarPricingRuleCommand command)
        {
            var id = await _mediator.Send(command); 
            return Ok(id);
        }

        [HttpPut("{id:int}")]
        //[Authorize(Policy = Permissions.CarPricingRules.Update)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCarPricingRuleCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var success = await _mediator.Send(command);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        //[Authorize(Policy = Permissions.CarPricingRules.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteCarPricingRuleCommand(id));

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllCarPricingRulesQuery());
            return Ok(result);
        }


        [HttpGet("{id:int}")]
        //[Authorize(Policy = Permissions.CarPricingRules.View)]
        public async Task<IActionResult> GetAsync(int id)
        {
            var rule = await _mediator.Send(new GetCarPricingRuleByIdQuery(id));

            if (rule == null)
                return NotFound();

            return Ok(rule);
        }

        [HttpGet("by-car/{carId:int}")]
        //[Authorize(Policy = Permissions.CarPricingRules.View)]
        public async Task<IActionResult> GetByCarId(int carId)
        {
            var rules = await _mediator.Send(new GetCarPricingRulesByCarIdQuery(carId));
            return Ok(rules);
        }
    }
}
