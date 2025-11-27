using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarModelsByBrandQueryHandler : IRequestHandler<GetCarModelsByBrandQuery, List<LookupDto>>
    {
        private readonly RentCarDbContext _context;

        public GetCarModelsByBrandQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<LookupDto>> Handle(GetCarModelsByBrandQuery request, CancellationToken cancellationToken)
        {
            return await _context.CarModels
                .Where(x => x.CarBrand.Id == request.BrandId)
                .Select(x => new LookupDto { Id = x.Id, Name = x.Name })
                .ToListAsync(cancellationToken);
        }
    }
}
