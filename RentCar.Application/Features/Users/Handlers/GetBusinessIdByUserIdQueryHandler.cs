using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Users.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Users.Handlers
{
    public class GetBusinessIdByUserIdQueryHandler
      : IRequestHandler<GetBusinessIdByUserIdQuery, Business?>
    {
        private readonly RentCarDbContext _context;

        public GetBusinessIdByUserIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<Business?> Handle(GetBusinessIdByUserIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Businesses
                .FirstOrDefaultAsync(b => b.UserId.ToString() == request.UserId, cancellationToken);
        }
    }
}
