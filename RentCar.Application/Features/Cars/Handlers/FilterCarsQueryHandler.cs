using MediatR;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class FilterCarsQueryHandler : IRequestHandler<FilterCarsQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;

        public FilterCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<CarDto>> Handle(FilterCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = await _carRepository.GetAllAsync();

            if (request.BrandId.HasValue)
                cars = cars.Where(c => c.CarModel.CarBrandId == request.BrandId.Value).ToList();

            if (request.TypeId.HasValue)
                cars = cars.Where(c => c.CarTypeId == request.TypeId.Value).ToList();

            if (request.FuelTypeId.HasValue)
                cars = cars.Where(c => c.FuelTypeId == request.FuelTypeId.Value).ToList();

            if (request.TransmissionId.HasValue)
                cars = cars.Where(c => c.TransmissionId == request.TransmissionId.Value).ToList();

            if (request.IsAvailable.HasValue)
                cars = cars.Where(c => c.IsAvailable == request.IsAvailable.Value).ToList();

            if (request.MinPrice.HasValue)
                cars = cars.Where(c => c.DailyPrice >= request.MinPrice.Value).ToList();

            if (request.MaxPrice.HasValue)
                cars = cars.Where(c => c.DailyPrice <= request.MaxPrice.Value).ToList();

            return cars.Select(c => new CarDto
            {
                Id = c.Id,
                LicensePlate = c.LicensePlate,
                DailyPrice = c.DailyPrice,
                // add others
            }).ToList();
        }
    }

}
