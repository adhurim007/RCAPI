using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs.Customer;
using RentCar.Application.Features.Customer.Commands;
using RentCar.Application.Features.Customer.Queries;

namespace RentCar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }

        // GET: api/customers/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery(id));
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCustomerCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, id);
        }

     
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerCommand command)
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
            var success = await _mediator.Send(new DeleteCustomerCommand(id));

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
