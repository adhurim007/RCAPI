using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs.MenuDto;
using RentCar.Application.Features.Menus.Commands;
using RentCar.Application.Features.Menus.Queries;
using RentCar.Domain.Authorization;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "SuperAdmin")] // only SuperAdmin can manage menus
    public class MenusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MenusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new menu
        /// </summary>
        [HttpPost("create")]
        //[Authorize(Policy = Permissions.Menus.Add)]
        public async Task<IActionResult> Create([FromBody] CreateMenuCommand command)
        {
            var menuId = await _mediator.Send(command);
            return Ok(new { MenuId = menuId });
        }

        /// <summary>
        /// Get menu by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetById(int id)
        {
            var result = await _mediator.Send(new GetMenuByIdQuery(id));

            if (result == null)
                return NotFound($"Menu with ID {id} not found.");

            return Ok(result);
        }

        /// <summary>
        /// Update an existing menu
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Policy = Permissions.Menus.Edit)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateMenuCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            var success = await _mediator.Send(command);
            return success ? Ok("Menu updated") : NotFound();
        }

        /// <summary>
        /// Delete a menu
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _mediator.Send(new DeleteMenuCommand(id));
            if (!success)
                return NotFound();

            // ✅ kthimi i duhur për DELETE
            return NoContent();
        }


        /// <summary>
        /// Get all menus (sorted)
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MenuDto>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllMenusQuery());
            if (result == null || !result.Any())
                return NotFound("No menus found");

            return Ok(result);
        }
  
        /// <summary>
        /// Assign a menu (claim) to a role
        /// </summary>
        [HttpPost("assign-to-role")]
        [Authorize(Policy = Permissions.Menus.AssignToRole)]
        public async Task<IActionResult> AssignToRole([FromBody] AssignMenuToRoleCommand command)
        {
            var success = await _mediator.Send(command);
            return success ? Ok("Menu assigned to role.") : BadRequest("Menu already assigned or role not found.");
        }

        [HttpGet("my-menus")]
        //[Authorize] // must be logged in
        public async Task<ActionResult<List<MenuDto>>> GetMyMenus()
        {
            var result = await _mediator.Send(new GetMenusForUserQuery());
            return Ok(result);
        }

        [HttpGet("claims")]
        public async Task<IActionResult> GetClaims([FromServices] RoleManager<IdentityRole<Guid>> roleManager)
        {
            // ✅ this returns all possible claims to bind to menu
            var claims = Permissions.All.Select(p => new { value = p, label = p });
            return Ok(claims);
        }
    }
}
