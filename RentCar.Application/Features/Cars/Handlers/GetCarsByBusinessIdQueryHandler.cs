using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.ReportingServices.Interfaces;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Application.MultiTenancy;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces.Repositories;
using RentCar.Persistence;
namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarsByBusinessIdQueryHandler
   : IRequestHandler<GetCarsByBusinessIdQuery, List<CarDto>>
    {
        private readonly RentCarDbContext _context;

        public GetCarsByBusinessIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarDto>> Handle(GetCarsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.Cars
                .Where(c => c.BusinessId == request.BusinessId)
                .Include(c => c.Business)
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
        }
    }

}
