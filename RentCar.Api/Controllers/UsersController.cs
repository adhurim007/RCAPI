using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.Roles.Queries;
using RentCar.Application.Features.Users.Command;
using RentCar.Application.Features.Users.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")] // only SuperAdmin can manage users
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId });
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _mediator.Send(new GetAllRolesQuery());
            return Ok(roles);
        }

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserCommand command)
        {
            var success = await _mediator.Send(command);
            return success ? Ok("Role assigned successfully.") : BadRequest("Failed to assign role.");
        }


        [HttpPost("{userId}/remove-role")]
        public async Task<IActionResult> RemoveRole(string userId, [FromBody] string role)
        {
            var result = await _mediator.Send(new RemoveRoleFromUserCommand(userId, role));
            return result ? Ok("Role removed.") : BadRequest("Failed to remove role.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            var result = await _mediator.Send(command);

            return Ok(new { message = "User updated successfully" });
        }

        [HttpPost("{id}/reset-password")]
        public async Task<IActionResult> ResetPassword(string id, [FromBody] ResetPasswordCommand command)
        {
            if (id != command.UserId)
                return BadRequest("UserId mismatch");

            var result = await _mediator.Send(command);

            //if (!result.Succeeded)
            //  return BadRequest(result.Errors);

            return Ok(new { message = "Password reset successfully" });
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _mediator.Send(new GetUserByIdQuery(id));

            if (user == null)
                return NotFound();

            return Ok(user);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _mediator.Send(new DeleteUserCommand(id));

            return Ok(new { message = "User deleted successfully" });
        }
    }
}
