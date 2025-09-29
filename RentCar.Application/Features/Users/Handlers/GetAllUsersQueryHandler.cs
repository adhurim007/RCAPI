using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Users;
using RentCar.Application.Features.Users.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;   // 👈 adjust if your DbContext namespace differs

namespace RentCar.Application.Features.Users.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RentCarDbContext _dbContext;

        public GetAllUsersQueryHandler(
            UserManager<ApplicationUser> userManager,
            RentCarDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.ToList();
            var result = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                string? businessName = null;
                if (roles.Contains("BusinessAdmin"))
                {
                    businessName = await _dbContext.Businesses
                        .Where(b => b.UserId == user.Id)
                        .Select(b => b.CompanyName)
                        .FirstOrDefaultAsync(cancellationToken);
                }

                result.Add(new UserDto
                {
                    Id = user.Id.ToString(),
                    FirstName = user.FullName ?? "",
                    Email = user.Email ?? "",
                    PhoneNumber = user.PhoneNumber ?? "",
                    BusinessName = !string.IsNullOrEmpty(businessName) ? businessName : "", 
                    Roles = roles.ToList(), 
                });
            }

            return result;
        }
    }
}
