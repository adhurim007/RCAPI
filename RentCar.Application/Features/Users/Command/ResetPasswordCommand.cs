using MediatR;
using Microsoft.AspNetCore.Identity;

namespace RentCar.Application.Features.Users.Command
{
    public record ResetPasswordCommand(string UserId, string NewPassword) : IRequest<IdentityResult>;
}
