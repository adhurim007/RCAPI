using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.Features.Menus.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System.Threading;
using System.Threading.Tasks;

public class AssignMenuToRoleCommandHandler : IRequestHandler<AssignMenuToRoleCommand, bool>
{
    private readonly RentCarDbContext _context;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public AssignMenuToRoleCommandHandler(RentCarDbContext context, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<bool> Handle(AssignMenuToRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null) return false;
         
        var existingClaims = await _roleManager.GetClaimsAsync(role);
        if (existingClaims.Any(c => c.Type == "Permission" && c.Value == request.Claim))
        {
            return false;  
        }
         
        var result = await _roleManager.AddClaimAsync(role, new System.Security.Claims.Claim("Permission", request.Claim));
        return result.Succeeded;
    }
}
