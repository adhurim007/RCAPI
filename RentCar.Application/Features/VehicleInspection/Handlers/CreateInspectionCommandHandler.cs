using MediatR;
using RentCar.Application.Features.VehicleInspection.Commands;

using RentCar.Persistence;

namespace RentCar.Application.Features.VehicleInspection.Handlers
{
    public class CreateInspectionCommandHandler : IRequestHandler<CreateInspectionCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateInspectionCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateInspectionCommand request, CancellationToken cancellationToken)
        {
            var inspection = new Domain.Entities.VehicleInspection
            {
                ReservationId = request.ReservationId,
                Type = (Domain.Enums.InspectionType)request.Type,
                Mileage = request.Mileage,
                FuelLevel = request.FuelLevel,
                TireCondition = request.TireCondition,
                OverallCondition = request.OverallCondition
            };

            _context.VehicleInspection.Add(inspection);
            await _context.SaveChangesAsync(cancellationToken);

            if (request.Photos != null)
            {
                foreach (var url in request.Photos)
                {
                    _context.VehicleInspectionPhoto.Add(new Domain.Entities.VehicleInspectionPhoto
                    {
                        InspectionId = inspection.Id,
                        ImageUrl = url
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);
            }

            return inspection.Id;
        }
    }

}
