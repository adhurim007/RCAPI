using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.Roles.Commands;
using RentCar.Application.Features.Roles.Queries;
using System;
using System.Threading.Tasks;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "SuperAdmin")] // enable later
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var success = await _mediator.Send(command);
            if (!success) return BadRequest(new { message = "Failed to create role." });

            return Ok(new { message = "Role created successfully." });
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateRoleCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { message = "ID mismatch" });

            var result = await _mediator.Send(command);
            if (!result)
                return NotFound(new { message = "Role not found" });

            return Ok(new { message = "Role updated successfully" });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteRoleCommand(id));
            if (!result)
                return NotFound(new { message = "Role not found" });

            return Ok(new { message = "Role deleted successfully" });
        }
         
    }
}
