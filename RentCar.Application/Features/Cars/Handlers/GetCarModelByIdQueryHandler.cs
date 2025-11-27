using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.CarModel;
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
    public class GetCarModelByIdQueryHandler : IRequestHandler<GetCarModelByIdQuery, CarModelDto?>
    {
        private readonly RentCarDbContext _context;

        public GetCarModelByIdQueryHandler(RentCarDbContext context) => _context = context;

        public async Task<CarModelDto?> Handle(GetCarModelByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.CarModels
                .Include(m => m.CarBrand)
                 .Select(m => new CarModelDto
                 {
                     Id = m.Id,
                     Name = m.Name,
                     CarBrandId = m.CarBrandId,
                     CarBrandName = m.CarBrand.Name
                 }).FirstOrDefaultAsync();
        }
    }
}
