using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RentCar.Application.Common.Identity;
using RentCar.Application.DTOs.Auth;
using RentCar.Application.Services;
using RentCar.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly AuthService _authService;

    public AuthController(UserManager<ApplicationUser> userManager,
        IConfiguration configuration, RoleManager<IdentityRole<Guid>> roleManager, AuthService authService)
    {
        _userManager = userManager;
        _configuration = configuration;
        _roleManager = roleManager;
        _authService = authService;
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
            Token = token,
            Email = user.Email,
            Roles = roles
        });
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, DefaultRoles.Client);
        return Ok("User registered successfully");

    }

    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }
}
