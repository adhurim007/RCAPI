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
    public class GetAllVehicleInspectionsQueryHandler :
        IRequestHandler<GetAllVehicleInspectionsQuery, List<VehicleInspectionDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllVehicleInspectionsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleInspectionDto>> Handle(
            GetAllVehicleInspectionsQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.VehicleInspection
                .Include(i => i.Reservation)
                    .ThenInclude(r => r.Car)
                        .ThenInclude(c => c.CarBrand)
                .Include(i => i.Reservation)
                    .ThenInclude(r => r.Car)
                        .ThenInclude(c => c.CarModel)
                .Include(i => i.Reservation)
                    .ThenInclude(r => r.Customer)
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

                    CarBrand = i.Reservation.Car.CarBrand.Name,
                    CarModel = i.Reservation.Car.CarModel.Name,
                    Customer = i.Reservation.Customer.FullName
                })
                .ToListAsync(cancellationToken);
        }
    }
}
