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
    public class RemoveRoleFromUserCommandHandler : IRequestHandler<RemoveRoleFromUserCommand, bool>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public RemoveRoleFromUserCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Handle(RemoveRoleFromUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null) return false;

            var result = await _userManager.RemoveFromRoleAsync(user, request.RoleName);
            return result.Succeeded;
        }
    }
}
