using MediatR;
using RentCar.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RentCar.Application.DTOs;

namespace RentCar.Application.Features.Cars.Queries.GetAllCars
{
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, List<CarDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllCarsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _context.Cars
                .Include(c => c.CarModel).ThenInclude(m => m.CarBrand)
                .Include(c => c.CarType)
                .Include(c => c.FuelType)
                .Include(c => c.Transmission)
                .Select(c => new CarDto
                {
                    Id = c.Id,
                    LicensePlate = c.LicensePlate,
                    Color = c.Color,
                    DailyPrice = c.DailyPrice,
                    Description = c.Description,
                    IsAvailable = c.IsAvailable,
                    ImageUrl = c.ImageUrl,
                    CarModelName = c.CarModel.Name,
                    CarBrandName = c.CarModel.CarBrand.Name,
                    CarTypeName = c.CarType.Name,
                    FuelTypeName = c.FuelType.Name,
                    TransmissionName = c.Transmission.Name
                })
                .ToListAsync(cancellationToken);

            return cars;
        }
    }
}
