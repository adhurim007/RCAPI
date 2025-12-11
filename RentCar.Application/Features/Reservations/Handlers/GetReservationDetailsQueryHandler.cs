using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Contract;
using RentCar.Application.DTOs.Payments;
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
    public class GetReservationDetailsQueryHandler
            : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto?>
    {
        private readonly RentCarDbContext _context;

        public GetReservationDetailsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDetailsDto?> Handle(
            GetReservationDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                 
                .Include(r => r.Car)
                    .ThenInclude(c => c.CarModel)
                        .ThenInclude(m => m.CarBrand) 
                .Include(r => r.Customer) 
                .Include(r => r.Business) 
                .Include(r => r.ReservationStatus) 
                .Include(r => r.Contract) 
                .Include(r => r.Payments) 
                .Include(r => r.PickupLocation)
                .Include(r => r.DropoffLocation) 
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId, cancellationToken);

            if (reservation == null)
                return null;

            return new ReservationDetailsDto
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
                Status = reservation.ReservationStatus.Name, 
                PickupLocationId = reservation.PickupLocationId,
                PickupLocation = reservation.PickupLocation.Name, 
                DropoffLocationId = reservation.DropoffLocationId,
                DropoffLocation = reservation.DropoffLocation.Name,

                 
            };
        }
    }
}
