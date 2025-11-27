using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.MenuDto;
using RentCar.Application.Features.Menus.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Handlers
{
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, List<MenuDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllMenusQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }
        public async Task<List<MenuDto>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return await _context.Menus
                .OrderBy(m => m.SortNumber)  
                .Select(m => new MenuDto
                {
                    Id = m.Id,
                    ParentId = m.ParentId,
                    Title = m.Title,
                    Subtitle = m.Subtitle,
                    Type = string.IsNullOrWhiteSpace(m.Type) ? "basic" : m.Type,
                    Icon = string.IsNullOrWhiteSpace(m.Icon) ? "heroicons_outline:square-3-stack-3d" : m.Icon,
                    Link = string.IsNullOrWhiteSpace(m.Link) ? "/" : m.Link,
                    HasSubMenu = m.HasSubMenu,
                    Claim = m.Claim,
                    Active = m.Active,
                    SortNumber = m.SortNumber
                })
                .ToListAsync(cancellationToken);
        }

    }
}
