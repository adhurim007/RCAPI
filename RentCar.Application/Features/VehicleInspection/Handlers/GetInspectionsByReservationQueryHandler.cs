using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.VehicleInspection;
using RentCar.Application.Features.VehicleInspection.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleInspection.Handlers
{
    public class GetInspectionsByReservationQueryHandler
        : IRequestHandler<GetInspectionsByReservationQuery, List<VehicleInspectionDto>>
    {
        private readonly RentCarDbContext _context;

        public GetInspectionsByReservationQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleInspectionDto>> Handle(
            GetInspectionsByReservationQuery request,
            CancellationToken cancellationToken)
        {
            var inspections = await _context.VehicleInspection
                .Where(i => i.ReservationId == request.ReservationId)
                .OrderByDescending(i => i.CreatedAt)
                .Select(i => new VehicleInspectionDto
                {
                    Id = i.Id,
                    ReservationId = i.ReservationId,
                    Type = (int)i.Type,
                    Mileage = i.Mileage,
                    FuelLevel = i.FuelLevel,
                    TireCondition = i.TireCondition,
                    OverallCondition = i.OverallCondition,
                    CreatedAt = i.CreatedAt,
                    Photos = i.Photos.Select(p => p.ImageUrl).ToList()
                })
                .ToListAsync(cancellationToken);

            return inspections;
        }
    }
}
