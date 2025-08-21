using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Cars.Queries.CarBrandAndModel;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetAllCarBrandsQueryHandler : IRequestHandler<GetAllCarBrandsQuery, List<CarBrand>>
    {
        private readonly RentCarDbContext _context;

        public GetAllCarBrandsQueryHandler(RentCarDbContext context) => _context = context;

        public async Task<List<CarBrand>> Handle(GetAllCarBrandsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CarBrands 
                .ToListAsync(cancellationToken);
        }
    }
}
