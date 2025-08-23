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
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, bool>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public CreateRoleCommandHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<bool> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            if (await _roleManager.RoleExistsAsync(request.RoleName))
                return false;

            var result = await _roleManager.CreateAsync(new IdentityRole<Guid>(request.RoleName));
            return result.Succeeded;
        }
    }
}
