using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Authorization;
using RentCar.Application.Features.Menus.Commands;
using RentCar.Application.Features.Menus.Queries;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")] // only SuperAdmin can manage menus
    public class MenusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        [Authorize(Policy = Permissions.Menus.Add)]
        public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
        {
            var menuId = await _mediator.Send(command);
            return Ok(new { MenuId = menuId });
        }

        [HttpPost("assign-to-role")]
        [Authorize(Policy = Permissions.Menus.AssignToRole)]
        public async Task<IActionResult> AssignToRole([FromBody] AssignMenuToRoleCommand command)
        {
            var success = await _mediator.Send(command);
            return success ? Ok("Menu assigned to role.") : BadRequest("Menu already assigned.");
        }

        [HttpGet("by-role/{roleId}")]
        [Authorize(Policy = Permissions.Menus.ViewByRole)]
        public async Task<IActionResult> GetMenusByRole(Guid roleId)
        {
            var menus = await _mediator.Send(new GetMenusByRoleQuery(roleId));
            return Ok(menus);
        }

        [HttpGet]
        public async Task<ActionResult<List<Menu>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllMenusQuery());
            return Ok(result);
        }
         

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuCommand command)
        {
            if (id != command.Id) return BadRequest("ID mismatch");
            var success = await _mediator.Send(command);
            return success ? Ok("Menu updated") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteMenuCommand(id));
            return success ? Ok("Menu deleted") : NotFound();
        }

    }
}
