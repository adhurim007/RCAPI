using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Authorization;
using RentCar.Application.Features.CarPricingRules.Command;
using RentCar.Application.Features.CarPricingRules.Queries;
using RentCar.Application.Features.Cars.Queries.GetAllCars;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Business")]
    public class CarPricingRulesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CarPricingRulesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Policy = Permissions.CarPricingRules.Create)]
        public async Task<IActionResult> Create([FromBody] CreateCarPricingRuleCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = result }, new { id = result });
        }

        [HttpPut("{id}")]
        [Authorize(Policy = Permissions.CarPricingRules.Update)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCarPricingRuleCommand command)
        {
            if (id != command.Id) return BadRequest("Id mismatch");
            var result = await _mediator.Send(command);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.CarPricingRules.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCarPricingRuleCommand(id));
            return result ? NoContent() : NotFound();
        }


        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.CarPricingRules.View)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var rule = await _mediator.Send(new GetCarPricingRuleByIdQuery(id));
            return rule == null ? NotFound() : Ok(rule);
        }

        [HttpGet("by-car/{carId}")]
        [Authorize(Policy = Permissions.CarPricingRules.View)]
        public async Task<IActionResult> GetByCarId(int carId)
        {
            var rules = await _mediator.Send(new GetCarPricingRulesByCarIdQuery(carId));
            return Ok(rules);
        } 
    }
}
