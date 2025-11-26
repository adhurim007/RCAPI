using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.MenuDto;
using RentCar.Application.Features.Menus.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic; 
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Handlers
{
    public class GetMenuByIdQueryHandler : IRequestHandler<GetMenuByIdQuery, MenuDto>
    {
        private readonly RentCarDbContext _context;

        public GetMenuByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<MenuDto> Handle(GetMenuByIdQuery request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus
                .Where(x => x.Id == request.Id)
                .Select(x => new MenuDto
                {
                    Id = x.Id,
                    ParentId = x.ParentId,
                    Title = x.Title,
                    Subtitle = x.Subtitle,
                    Type = x.Type,
                    Icon = x.Icon,
                    Link = x.Link,
                    HasSubMenu = x.HasSubMenu,
                    Active = x.Active,
                    Claim = x.Claim,
                    SortNumber = x.SortNumber
                }).FirstOrDefaultAsync(cancellationToken);

            return menu;
        }
    }

}
