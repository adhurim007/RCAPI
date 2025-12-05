using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Customer.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Customer.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateCustomerCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Customer
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return false;
             
            entity.FullName = request.FullName;
            entity.Email = request.Email;
            entity.PhoneNumber = request.PhoneNumber;
            entity.DocumentType = request.DocumentType;
            entity.DocumentNumber = request.DocumentNumber;
            entity.DateOfBirth = request.DateOfBirth;
            entity.Address = request.Address;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
