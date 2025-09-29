using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.Features.Users.Command;
using RentCar.Domain.Entities;

namespace RentCar.Application.Features.Users.Handlers
{
    public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, IdentityResult>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found" });

            // Generate reset token
            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

            // Reset password
            var result = await _userManager.ResetPasswordAsync(user, resetToken, request.NewPassword);

            return result;
        }
    }
}
