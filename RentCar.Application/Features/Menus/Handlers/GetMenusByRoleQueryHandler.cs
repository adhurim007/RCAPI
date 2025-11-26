using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.MenuDto;
using RentCar.Application.Features.Menus.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
 

public class GetMenusForUserQueryHandler : IRequestHandler<GetMenusForUserQuery, List<MenuDto>>
{
    private readonly RentCarDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
  
    public GetMenusForUserQueryHandler(RentCarDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<List<MenuDto>> Handle(GetMenusForUserQuery request, CancellationToken cancellationToken)
    {
        var userClaims = _httpContextAccessor.HttpContext.User.Claims
            .Where(c => c.Type == "Permission")
            .Select(c => c.Value)
            .ToList();

        var menus = await _context.Menus
            .Where(m => string.IsNullOrEmpty(m.Claim) || userClaims.Contains(m.Claim))
            .Select(m => new MenuDto
            {
                Id = m.Id,
                ParentId = m.ParentId,
                Title = m.Title,
                Type = m.Type,
                Icon = m.Icon,
                Link = m.Link
            })
            .ToListAsync(cancellationToken);

        return menus;
    }
}

