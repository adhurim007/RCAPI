using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Customer;
using RentCar.Application.Features.Customer.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Customer.Handlers
{
    public class GetCustomerByIdQueryHandler
            : IRequestHandler<GetCustomerByIdQuery, CustomerDto?>
    {
        private readonly RentCarDbContext _context;

        public GetCustomerByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto?> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Customer
                .Where(x => x.Id == request.Id)
                .Select(x => new CustomerDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FullName = x.FullName,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    DocumentType = x.DocumentType,
                    DocumentNumber = x.DocumentNumber,
                    DateOfBirth = x.DateOfBirth.ToString("yyyy-MM-dd"),
                    Address = x.Address
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
