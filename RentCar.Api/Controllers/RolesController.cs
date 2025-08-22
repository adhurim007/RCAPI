using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.Roles.Commands;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")] // only SuperAdmin can manage roles
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleCommand command)
        {
            var success = await _mediator.Send(command);
            return success ? Ok("Role created successfully.") : BadRequest("Failed to create role.");
        }
    }
}
