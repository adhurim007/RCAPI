using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.Features.Users.Command;
using RentCar.Domain.Entities;
using RentCar.Persistence;

namespace RentCar.Application.Features.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, string>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly RentCarDbContext _dbContext;

        public CreateUserCommandHandler(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            RentCarDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                PhoneNumber = request.Phone,
                FullName = $"{request.FirstName} {request.LastName}"
            };

            // 1. Create the user
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                throw new ApplicationException(string.Join(", ", result.Errors.Select(e => e.Description)));

            // 2. Assign role
            if (!await _roleManager.RoleExistsAsync(request.Role))
                throw new ApplicationException($"Role '{request.Role}' does not exist.");

            await _userManager.AddToRoleAsync(user, request.Role);

            // 3. If BusinessAdmin → Create Business entry
            if (request.Role == "BusinessAdmin")
            {
                var business = new Business
                {
                    UserId = user.Id,                     // FK link
                    CompanyName = request.CompanyName ?? "",
                    ContactPhone = request.ContactPhone ?? "",
                    Address = request.Address ?? "",
                    CityId = request.CityId ?? 0,
                    StateId = request.StateId ?? 0,
                    IsActive = true,                      // default true
                    IsApproved = true,                    // default true
                    ApprovedBy = null,
                    ApprovedDate = null
                };

                _dbContext.Businesses.Add(business);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }


            return user.Id.ToString();
        }
    }
}
