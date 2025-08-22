using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.Features.Roles.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Roles.Handlers
{
    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, List<IdentityRole<Guid>>>
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public GetAllRolesQueryHandler(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public Task<List<IdentityRole<Guid>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_roleManager.Roles.ToList());
        }
    }
}
