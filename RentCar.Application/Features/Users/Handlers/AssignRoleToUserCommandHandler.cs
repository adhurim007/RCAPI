using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.Features.Users.Command;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Users.Handlers
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignRoleToUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(AssignRoleToUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) throw new KeyNotFoundException("User not found");

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            return result.Succeeded;
        }
    }
}
