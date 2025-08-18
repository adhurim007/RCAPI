using MediatR;
using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommand, bool>
    {
        private readonly ICarRepository _carRepository;

        public UpdateCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<bool> Handle(UpdateCarCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdAsync(request.Id);

            if (car == null)
                return false;

            car.LicensePlate = request.LicensePlate;
            car.Color = request.Color;
            car.DailyPrice = request.DailyPrice;
            car.CarModelId = request.CarModelId;
            car.BusinessId = request.BusinessId;

            await _carRepository.UpdateAsync(car);
            return true;
        }
    }
}
