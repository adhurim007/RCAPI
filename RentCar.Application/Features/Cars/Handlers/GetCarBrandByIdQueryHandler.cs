using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarBrandByIdQueryHandler : IRequestHandler<GetCarBrandByIdQuery, CarBrand?>
    {
        private readonly RentCarDbContext _context;

        public GetCarBrandByIdQueryHandler(RentCarDbContext context) => _context = context;

        public async Task<CarBrand?> Handle(GetCarBrandByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.CarBrands 
                .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        }
    }
}
