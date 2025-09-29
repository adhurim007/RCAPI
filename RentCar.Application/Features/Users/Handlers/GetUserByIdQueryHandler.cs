using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Users;
using RentCar.Application.Features.Users.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;

namespace RentCar.Application.Features.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RentCarDbContext _dbContext;

        public GetUserByIdQueryHandler(UserManager<ApplicationUser> userManager, RentCarDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == request.Id, cancellationToken);

            if (user == null)
                return null;

            var roles = await _userManager.GetRolesAsync(user);

            // load business if user is BusinessAdmin
            Business? business = null;
            if (roles.Contains("BusinessAdmin"))
            {
                business = await _dbContext.Businesses
                    .FirstOrDefaultAsync(b => b.UserId == user.Id, cancellationToken);
            }

            var userDto = new UserDto
            {
                Id = user.Id.ToString(),
                FirstName = user.FullName,       // ✅ fixed
                LastName = user.FullName,         // ✅ mapped
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = roles.ToList(),

                // Business fields
                BusinessName = business?.CompanyName,
                ContactPhone = business?.ContactPhone,
                Address = business?.Address,
                StateId = business?.StateId,
                CityId = business?.CityId
            };

            return userDto;
        }
    }
}
