using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.VehicleInspection;
using RentCar.Application.Features.VehicleInspection.Queries;
using RentCar.Persistence;

namespace RentCar.Application.Features.VehicleInspection.Handlers
{
    public class GetInspectionByIdQueryHandler : IRequestHandler<GetInspectionByIdQuery, VehicleInspectionDto>
    {
        private readonly RentCarDbContext _context;

        public GetInspectionByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleInspectionDto> Handle(GetInspectionByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.VehicleInspection
                .Where(v => v.Id == request.Id)
                .Select(v => new VehicleInspectionDto
                {
                    Id = v.Id,
                    ReservationId = v.ReservationId,
                    Type = (int)v.Type,
                    Mileage = v.Mileage,
                    FuelLevel = v.FuelLevel,
                    TireCondition = v.TireCondition,
                    OverallCondition = v.OverallCondition,
                    CreatedAt = v.CreatedAt,
                    Photos = v.Photos.Select(p => p.ImageUrl).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }

}
