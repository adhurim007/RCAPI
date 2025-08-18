using MediatR;
using RentCar.Application.Features.Cars.Commands; 
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommand, Guid>
    {
        private readonly ICarRepository _carRepository;

        public CreateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = new Car
            { 
                BusinessId = request.BusinessId,
                CarModelId = request.CarModelId,
                CarTypeId = request.CarTypeId,
                FuelTypeId = request.FuelTypeId,
                TransmissionId = request.TransmissionId,
                LicensePlate = request.LicensePlate,
                Color = request.Color,
                DailyPrice = request.DailyPrice,
                ImageUrl = request.ImageUrl,
                Description = request.Description,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow
            };

            await _carRepository.AddAsync(car);
            return car.Id;
        }
    }
}
