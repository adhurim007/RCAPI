using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Menus.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public class GetMenusByRoleQueryHandler : IRequestHandler<GetMenusByRoleQuery, List<Menu>>
{
    private readonly RentCarDbContext _context;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public GetMenusByRoleQueryHandler(RentCarDbContext context, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _context = context;
        _roleManager = roleManager;
    }

    public async Task<List<Menu>> Handle(GetMenusByRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        if (role == null) return new List<Menu>();

        var claims = await _roleManager.GetClaimsAsync(role);
        var claimValues = claims.Where(c => c.Type == "Permission").Select(c => c.Value).ToList();

        return await _context.Menus
            .Where(m => m.Active && claimValues.Contains(m.Claim))
            .OrderBy(m => m.SortNumber)
            .ToListAsync(cancellationToken);
    }
}
