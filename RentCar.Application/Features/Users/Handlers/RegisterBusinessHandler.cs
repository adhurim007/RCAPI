using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.DTOs.Auth;
using RentCar.Application.Features.Users.Command;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Users.Handlers
{
    public class RegisterBusinessHandler : IRequestHandler<RegisterBusinessCommand, int>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly RentCarDbContext _dbContext;

        public RegisterBusinessHandler(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            RentCarDbContext dbContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
        }

        public async Task<int> Handle(RegisterBusinessCommand request, CancellationToken cancellationToken)
        {
            var dto = request.Dto;

           
            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join("; ", result.Errors.Select(e => e.Description)));
            }

          
            if (!await _roleManager.RoleExistsAsync("BusinessAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole<Guid>("BusinessAdmin"));
            }
            await _userManager.AddToRoleAsync(user, "BusinessAdmin");
             
            var business = new Business
            {
                UserId = user.Id,   
                CompanyName = dto.CompanyName,
                TaxId = dto.TaxId ?? string.Empty,
                ContactPhone = dto.ContactPhone,
                CityId = dto.CityId,
                StateId = dto.StateId,
                Address = dto.Address,
                IsActive = true,
                IsApproved = false
            };

            _dbContext.Businesses.Add(business);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return business.Id;
        }
    }

}
