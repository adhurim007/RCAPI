using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.DTOs.Auth;
using RentCar.Application.Features.Users.Command;
using RentCar.Domain.Entities;
using static RentCar.Persistence.Identity.RoleSeeder;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly AuthService _authService;
    private readonly IMediator _mediator;

    public AuthController(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        AuthService authService,
        IMediator mediator)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _authService = authService; 
        _mediator = mediator;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Unauthorized(new { message = "Wrong email or password" });

        var token = await _authService.GenerateJwtToken(user);
        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new
        {
            token,
            email = user.Email,
            fullName = user.FullName,
            roles
        });
    }

    [HttpPost("register-business")]
    public async Task<IActionResult> RegisterBusiness([FromBody] RegisterBusinessDto dto)
    {
        var businessId = await _mediator.Send(new RegisterBusinessCommand(dto));
        return Ok(new { Message = "Business registered successfully", BusinessId = businessId });
    }
     

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Email,
            Email = model.Email,
            //FullName = model.FullName
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, DefaultRoles.Client);
        return Ok("User registered successfully");
    }
}
