using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RentCar.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly IConfiguration _config;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager,
        IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
    }

    //public async Task<string> GenerateJwtToken(ApplicationUser user)
    //{
    //    // User claims (direct claims added to user)
    //    var userClaims = await _userManager.GetClaimsAsync(user);

    //    // Roles
    //    var roles = await _userManager.GetRolesAsync(user);

    //    // Convert role names into claims
    //    var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r)).ToList();

    //    // 🔹 Collect all role-based claims (from AspNetRoleClaims)
    //    var allRoleClaims = new List<Claim>();
    //    foreach (var roleName in roles)
    //    {
    //        var role = await _roleManager.FindByNameAsync(roleName);
    //        if (role != null)
    //        {
    //            var claimss = await _roleManager.GetClaimsAsync(role);
    //            allRoleClaims.AddRange(claimss);
    //        }
    //    }

    //    // Base claims (always present)
    //    var claims = new List<Claim>
    //    {
    //        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
    //        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
    //        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    //    };

    //    // Merge all claims
    //    claims.AddRange(userClaims);
    //    claims.AddRange(roleClaims);
    //    claims.AddRange(allRoleClaims);

    //    // Create signing key
    //    var key = new SymmetricSecurityKey(
    //        Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
    //    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    //    // Create token
    //    var token = new JwtSecurityToken(
    //        issuer: _config["JwtSettings:Issuer"],
    //        audience: _config["JwtSettings:Audience"],
    //        claims: claims,
    //        expires: DateTime.UtcNow.AddMinutes(
    //            Convert.ToDouble(_config["JwtSettings:DurationInMinutes"])
    //        ),
    //        signingCredentials: creds
    //    );

    //    return new JwtSecurityTokenHandler().WriteToken(token);
    //}

    public async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        // User claims
        var userClaims = await _userManager.GetClaimsAsync(user);

        // Roles
        var roles = await _userManager.GetRolesAsync(user);
        var roleClaims = new List<Claim>();

        foreach (var roleName in roles)
        {
            // Add role as ClaimTypes.Role
            roleClaims.Add(new Claim(ClaimTypes.Role, roleName));

            // Get claims assigned to this role (from AspNetRoleClaims)
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                var claimss = await _roleManager.GetClaimsAsync(role);
                roleClaims.AddRange(claimss);
            }
        }

        // Merge everything
        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    }
        .Union(userClaims)
        .Union(roleClaims);

        // Sign key
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["JwtSettings:DurationInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


}
