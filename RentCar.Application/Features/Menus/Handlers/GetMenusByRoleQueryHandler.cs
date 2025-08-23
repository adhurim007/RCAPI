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
    public class GetMenusByRoleQueryHandler : IRequestHandler<GetMenusByRoleQuery, List<Menu>>
    {
        private readonly RentCarDbContext _db;

        public GetMenusByRoleQueryHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<List<Menu>> Handle(GetMenusByRoleQuery request, CancellationToken cancellationToken)
        {
            return await _db.RoleMenus
                .Where(rm => rm.RoleId == request.RoleId)
                .Include(rm => rm.Menu)
                .Select(rm => rm.Menu)
                .ToListAsync(cancellationToken);
        }
    }
}
