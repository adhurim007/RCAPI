using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
        {
            private readonly RentCarDbContext _context;
            private readonly UserManager<ApplicationUser> _userManager;

            public DeleteCustomerCommandHandler(
                RentCarDbContext context,
                UserManager<ApplicationUser> userManager)
            {
                _context = context;
                _userManager = userManager;
            }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customer
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (customer == null)
                return false;
             
            var userId = customer.UserId;
             
            var roles = _context.UserRoles.Where(x => x.UserId == userId);
            _context.UserRoles.RemoveRange(roles);
             
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
            {
                _context.Users.Remove(user);
            }
             
            _context.Customer.Remove(customer);
             
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        } 
    }
}
