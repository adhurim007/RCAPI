using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.Features.Roles.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Roles.Handlers
{
    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, bool>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public UpdateRoleCommandHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id);
            if (role == null) return false;

            role.Name = request.Name;
            var result = await _roleManager.UpdateAsync(role);

            return result.Succeeded;
        }
    }
}
