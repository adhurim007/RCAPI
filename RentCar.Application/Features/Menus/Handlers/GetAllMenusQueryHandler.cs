using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class GetAllMenusQueryHandler : IRequestHandler<GetAllMenusQuery, List<Menu>>
    {
        private readonly RentCarDbContext _context;

        public GetAllMenusQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> Handle(GetAllMenusQuery request, CancellationToken cancellationToken)
        {
            return await _context.Menus
                .Include(m => m.RoleMenus)
                .ToListAsync(cancellationToken);
        }
    }
}
