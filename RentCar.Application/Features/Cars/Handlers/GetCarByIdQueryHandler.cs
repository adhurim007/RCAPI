using MediatR;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces.Repositories;

namespace RentCar.Application.Features.Cars.Handlers
{ 
    public class GetCarByIdQueryHandler : IRequestHandler<GetCarByIdQuery, CarDto>
    {
        private readonly ICarRepository _repository;

        public GetCarByIdQueryHandler(ICarRepository repository)
        {
            _repository = repository;
        }

        public async Task<CarDto> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
        {
            var car = await _repository.GetByIdAsync(request.Id);

            if (car == null)
                throw new Exception();

            return new CarDto
            {
                Id = car.Id,
                BusinessId = car.BusinessId,
                CarBrandId = car.CarBrandId,
                CarModelId = car.CarModelId,
                CarTypeId = car.CarTypeId,
                FuelTypeId = car.FuelTypeId,
                TransmissionId = car.TransmissionId,
                LicensePlate = car.LicensePlate,
                Color = car.Color,
                DailyPrice = car.DailyPrice,
                Description = car.Description,
                IsAvailable = car.IsAvailable
            };
        }
    }

}
