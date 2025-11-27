using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.CarBrandAndModel;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarBrandsQueryHandler : IRequestHandler<GetCarBrandsQuery, List<LookupDto>>
    {
        private readonly RentCarDbContext _context;

        public GetCarBrandsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<LookupDto>> Handle(GetCarBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CarBrands
                .Select(x => new LookupDto { Id = x.Id, Name = x.Name })
                .ToListAsync(cancellationToken);
        }
    }
}
