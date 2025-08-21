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
    public class GetAllCarModelsQueryHandler : IRequestHandler<GetAllCarModelsQuery, List<CarModel>>
    {
        private readonly RentCarDbContext _context;

        public GetAllCarModelsQueryHandler(RentCarDbContext context) => _context = context;

        public async Task<List<CarModel>> Handle(GetAllCarModelsQuery request, CancellationToken cancellationToken)
        {
            return await _context.CarModels
                .Include(m => m.CarBrand)
                //.Include(m => m.Cars)
                .ToListAsync(cancellationToken);
        }
    }
}
