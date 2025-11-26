using MediatR;
using RentCar.Application.Features.Cars.Handlers;
using RentCar.Domain.Interfaces.Repositories;

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

        car.BusinessId = request.BusinessId;
        car.CarBrandId = request.CarBrandId;
        car.CarModelId = request.CarModelId;
        car.CarTypeId = request.CarTypeId;
        car.FuelTypeId = request.FuelTypeId;
        car.TransmissionId = request.TransmissionId;

        car.LicensePlate = request.LicensePlate;
        car.Color = request.Color;
        car.DailyPrice = request.DailyPrice;
        car.IsAvailable = request.IsAvailable;
        car.Description = request.Description;

        await _carRepository.UpdateAsync(car);
        return true;
    }
}
