using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Application.MultiTenancy;
using RentCar.Domain.Interfaces.Repositories;
namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarsByBusinessIdQueryHandler : IRequestHandler<GetCarsByBusinessIdQuery, List<CarDto>>
    {
        private readonly ICarRepository _carRepository;
        private readonly ITenantProvider _tenantProvider;

        public GetCarsByBusinessIdQueryHandler(ICarRepository carRepository, ITenantProvider tenantProvider)
        {
            _carRepository = carRepository;
            _tenantProvider = tenantProvider;
        }

        public async Task<List<CarDto>> Handle(GetCarsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var businessId = _tenantProvider.GetBusinessId();

            if (!_tenantProvider.IsSuperAdmin() && (!businessId.HasValue || businessId.Value != request.BusinessId))
                throw new UnauthorizedAccessException("You are not allowed to access these cars.");

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
