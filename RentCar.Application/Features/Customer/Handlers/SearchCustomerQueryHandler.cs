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
    public class SearchCustomerQueryHandler : IRequestHandler<SearchCustomerQuery, CustomerDto>
    {
        private readonly RentCarDbContext _context;

        public SearchCustomerQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDto> Handle(SearchCustomerQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customer.FirstOrDefaultAsync(x => x.DocumentNumber == request.PersonalNumber, cancellationToken);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                FullName = customer.FullName,
                DocumentNumber = customer.DocumentNumber,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };
        }
    }
}
