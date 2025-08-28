using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Common.Identity;
using RentCar.Application.DTOs.Auth;
using RentCar.Domain.Entities;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly AuthService _authService;

    public AuthController(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        AuthService authService)
    {
        _userManager = userManager;
        _roleManager = roleManager; 
        _authService = authService;
    }

    [HttpPost("signin")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
            return Unauthorized(new { message = "Wrong email or password" });

        var tokenResult = await _authService.GenerateJwtToken(user);

        return Ok(tokenResult); // include Token, ExpiresAt, Roles, maybe RefreshToken
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

    //[HttpPost("forgot-password")]
    //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    //{
    //    var user = await _userManager.FindByEmailAsync(model.Email);
    //    if (user == null)
    //        return BadRequest("User not found");

    //    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
    //    // TODO: send email with token
    //    return Ok("Password reset link sent.");
    //}

    //[HttpPost("reset-password")]
    //public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    //{
    //    var user = await _userManager.FindByEmailAsync(model.Email);
    //    if (user == null)
    //        return BadRequest("User not found");

    //    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.NewPassword);
    //    if (!result.Succeeded)
    //        return BadRequest(result.Errors);

    //    return Ok("Password reset successful.");
    //}
}
