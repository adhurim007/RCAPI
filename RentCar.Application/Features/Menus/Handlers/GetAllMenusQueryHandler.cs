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
                .Select(m => new MenuDto
                {
                    Id = m.Id,
                    Title = m.Title, // map correctly from your entity field
                    Type = string.IsNullOrEmpty(m.Type) ? "basic" : m.Type,
                    Icon = string.IsNullOrEmpty(m.Icon) ? "heroicons_outline:square-3-stack-3d" : m.Icon,
                    Link = string.IsNullOrEmpty(m.Subtitle) ? "/" : m.Subtitle
                })
                .ToListAsync(cancellationToken);
        }
    }
}
