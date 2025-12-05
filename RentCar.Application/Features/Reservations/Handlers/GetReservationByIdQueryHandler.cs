using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class GetReservationByIdQueryHandler
       : IRequestHandler<GetReservationByIdQuery, ReservationDto?>
    {
        private readonly RentCarDbContext _context;

        public GetReservationByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDto?> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations

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

                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return null;

            return new ReservationDto
            {
                Id = reservation.Id,
                ReservationNumber = reservation.ReservationNumber,

                CarId = reservation.CarId,
                CarBrand = reservation.Car.CarModel.CarBrand.Name,
                CarModel = reservation.Car.CarModel.Name,
                LicensePlate = reservation.Car.LicensePlate,

                CustomerId = reservation.CustomerId,
                CustomerName = reservation.Customer.FullName,

                BusinessId = reservation.BusinessId,
                BusinessName = reservation.Business.CompanyName,

                PickupDate = reservation.PickupDate,
                DropoffDate = reservation.DropoffDate,
                TotalDays = reservation.TotalDays,

                TotalPrice = reservation.TotalPrice,
                DepositAmount = (decimal)reservation.DepositAmount,

                PaymentStatus = reservation.PaymentStatus.ToString(),
                DepositStatus = reservation.DepositStatus.ToString(),

                ReservationStatusId = reservation.ReservationStatusId,
                Status = reservation.ReservationStatus.Name,

                PickupLocationId = reservation.PickupLocationId,
                PickupLocation = reservation.PickupLocation.Name,

                DropoffLocationId = reservation.DropoffLocationId,
                DropoffLocation = reservation.DropoffLocation.Name,

                Notes = reservation.Notes
            };
        }
    }
}
