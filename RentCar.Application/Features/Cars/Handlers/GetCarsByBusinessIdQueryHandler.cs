using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Interfaces.Repositories;
namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarsByBusinessIdQueryHandler : IRequestHandler<GetCarsByBusinessIdQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;

        public GetCarsByBusinessIdQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarDto>> Handle(GetCarsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository
                .Query()
                .Where(c => c.BusinessId == request.BusinessId)
                .Include(c => c.CarModel)
                    .ThenInclude(m => m.CarBrand)
                .Select(c => new CarDto
                {
                    Id = c.Id,
                    LicensePlate = c.LicensePlate,
                    Color = c.Color,
                    DailyPrice = c.DailyPrice,
                    CarModel = c.CarModel.Name,
                    CarBrand = c.CarModel.CarBrand.Name
                })
                .ToListAsync(cancellationToken);

            return cars;
        }
    }
}
