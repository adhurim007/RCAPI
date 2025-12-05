using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Application.MultiTenancy;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class GetReservationsByBusinessIdQueryHandler
       : IRequestHandler<GetReservationsByBusinessIdQuery, List<ReservationDto>>
    {
        private readonly RentCarDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public GetReservationsByBusinessIdQueryHandler(
            RentCarDbContext context,
            ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<List<ReservationDto>> Handle(
            GetReservationsByBusinessIdQuery request,
            CancellationToken cancellationToken)
        {
            var currentBusinessId = _tenantProvider.GetBusinessId();

            // Super Admin mund t'i sheh krejt bizneset
            if (!_tenantProvider.IsSuperAdmin() && currentBusinessId != request.BusinessId)
                throw new UnauthorizedAccessException("Not allowed to view these reservations.");

            var reservations = await _context.Reservations

                // Car → Model → Brand
                .Include(r => r.Car)
                    .ThenInclude(c => c.CarModel)
                        .ThenInclude(m => m.CarBrand)

                // Customer
                .Include(r => r.Customer)

                // Business
                .Include(r => r.Business)

                // Status
                .Include(r => r.ReservationStatus)

                // Locations
                .Include(r => r.PickupLocation)
                .Include(r => r.DropoffLocation)

                .Where(r => r.BusinessId == request.BusinessId)
                .ToListAsync(cancellationToken);

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                ReservationNumber = r.ReservationNumber,

                CarId = r.CarId,
                CarBrand = r.Car.CarModel.CarBrand.Name,
                CarModel = r.Car.CarModel.Name,
                LicensePlate = r.Car.LicensePlate,

                CustomerId = r.CustomerId,
                CustomerName = r.Customer.FullName,

                BusinessId = r.BusinessId,
                BusinessName = r.Business.CompanyName,

                PickupDate = r.PickupDate,
                DropoffDate = r.DropoffDate,
                TotalDays = r.TotalDays,

                TotalPrice = r.TotalPrice,
                DepositAmount = (decimal)r.DepositAmount,

                PaymentStatus = r.PaymentStatus.ToString(),
                DepositStatus = r.DepositStatus.ToString(),

                ReservationStatusId = r.ReservationStatusId,
                Status = r.ReservationStatus.Name,

                PickupLocationId = r.PickupLocationId,
                PickupLocation = r.PickupLocation.Name,

                DropoffLocationId = r.DropoffLocationId,
                DropoffLocation = r.DropoffLocation.Name,

                Notes = r.Notes

            }).ToList();
        }
    }
}
