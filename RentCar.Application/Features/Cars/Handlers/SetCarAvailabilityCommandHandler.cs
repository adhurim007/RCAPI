using MediatR;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Interfaces;
using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class SetCarAvailabilityCommandHandler : IRequestHandler<SetCarAvailabilityCommand, bool>
    {
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetCarAvailabilityCommandHandler(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(SetCarAvailabilityCommand request, CancellationToken cancellationToken)
        {
            var car = await _carRepository.GetByIdAsync(request.CarId);
            if (car == null) return false;

            car.IsAvailable = request.IsAvailable;
            await _carRepository.UpdateAsync(car);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}
