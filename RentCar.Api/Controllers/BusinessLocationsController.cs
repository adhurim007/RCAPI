using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs.BusinessLocationDTO;
using RentCar.Application.Features.BusinessLocations.Command;
using RentCar.Application.Features.BusinessLocations.Queries;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BusinessLocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BusinessLocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllBusinessLocationsQuery());
            return Ok(result);
        }

        [HttpGet("by-business/{businessId:int}")]
        public async Task<IActionResult> GetByBusiness(int businessId)
        {
            var result = await _mediator.Send(
                new GetBusinessLocationsByBusinessIdQuery(businessId)
            );

            return Ok(result);
        }


        [HttpGet("businesses")]
        public async Task<ActionResult<List<BusinessDto>>> GetBusinesses()
        {
            var result = await _mediator.Send(new GetAllBusinessesQuery());
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] int businessId)
        {
            var result = await _mediator.Send(new GetBusinessLocationsQuery(businessId));
            return Ok(result);
        }
 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetBusinessLocationByIdQuery(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBusinessLocationCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBusinessLocationCommand command)
        {
            if (id != command.Id)
                return BadRequest("Id mismatch");

            var success = await _mediator.Send(command);
            if (!success)
                return NotFound();

            return NoContent();
        }
 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteBusinessLocationCommand(id));
            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
