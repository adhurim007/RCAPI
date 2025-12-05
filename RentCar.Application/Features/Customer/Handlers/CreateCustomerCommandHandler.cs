using MediatR;
using Microsoft.AspNetCore.Identity;
using RentCar.Application.Features.Customer.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Customer.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly RentCarDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateCustomerCommandHandler(RentCarDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        { 
            var baseUsername = request.FullName
                .Trim()
                .ToLower()
                .Replace(" ", ".");      

            var randomSuffix = new Random().Next(1000, 9999);

            var username = $"{baseUsername}.{randomSuffix}";
             
            var password = "Client@123";
             
            var user = new ApplicationUser
            {
                UserName = username,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
             
            await _userManager.AddToRoleAsync(user, "Customer");
             
            var customer = new Domain.Entities.Customer
            {
                UserId = user.Id,
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                DocumentType = request.DocumentType,
                DocumentNumber = request.DocumentNumber,
                DateOfBirth = request.DateOfBirth,
                Address = request.Address
            };

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);

            return customer.Id;
        } 
    }
}
