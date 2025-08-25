using MediatR;
using RentCar.Application.Features.Menus.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Handlers
{
    public class AssignMenuToRoleCommandHandler : IRequestHandler<AssignMenuToRoleCommand, bool>
    {
        private readonly RentCarDbContext _db;

        public AssignMenuToRoleCommandHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(AssignMenuToRoleCommand request, CancellationToken cancellationToken)
        {
            var exists = _db.RoleMenus
                .Any(rm => rm.RoleId == request.RoleId && rm.MenuId == request.MenuId);

            if (exists) return false; // already assigned

            var roleMenu = new RoleMenu
            {
                RoleId = request.RoleId,
                MenuId = request.MenuId
            };

            _db.RoleMenus.Add(roleMenu);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
