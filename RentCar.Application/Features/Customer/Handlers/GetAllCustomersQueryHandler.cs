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
    public class GetAllCustomersQueryHandler
        : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllCustomersQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customer.ToListAsync(cancellationToken);
            var result = new List<CustomerDto>();

            foreach (var c in customers)
            {
                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Id == c.UserId, cancellationToken);

                result.Add(new CustomerDto
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    Email = c.Email ?? "",
                    PhoneNumber = c.PhoneNumber ?? "",
                    DocumentType = c.DocumentType,
                    DocumentNumber = c.DocumentNumber,
                    DateOfBirth = c.DateOfBirth.ToString("yyyy-MM-dd"),
                    Address = c.Address
                });
            }

            return result;
        }

    }
}
