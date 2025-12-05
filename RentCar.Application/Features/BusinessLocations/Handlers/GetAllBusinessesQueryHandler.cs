using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.BusinessLocationDTO;
using RentCar.Application.Features.BusinessLocations.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Handlers
{
    public class GetAllBusinessesQueryHandler
        : IRequestHandler<GetAllBusinessesQuery, List<BusinessDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllBusinessesQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<BusinessDto>> Handle(GetAllBusinessesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Businesses
                .Select(x => new BusinessDto
                {
                    Id = x.Id,
                    CompanyName = x.CompanyName, 
                })
                .ToListAsync(cancellationToken);
        }
    }
}
