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

                // Contract
                .Include(r => r.Contract)

                // Payments (many)
                .Include(r => r.Payments)

                // Locations
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

                //Payments = reservation.Payments.Select(p => new PaymentDto
                //{
                //    Id = p.Id,
                //    Amount = p.Amount,
                //    PaidAt = p.PaidAt,
                //    PaymentMethod = p.PaymentMethod
                //}).ToList(),

                //Contract = reservation.Contract != null
                //    ? new ContractDto
                //    {
                //        Id = reservation.Contract.Id,
                //        FileUrl = reservation.Contract.FileUrl,
                //        CreatedAt = reservation.Contract.CreatedAt
                //    }
                //    : null,

                //StatusHistory = reservation.ReservationStatusHistories
                //    .OrderByDescending(h => h.ChangedAt)
                //    .Select(h => new ReservationStatusHistoryDto
                //    {
                //        Id = h.Id,
                //        ReservationId = h.ReservationId,
                //        ReservationStatusId = h.ReservationStatusId,
                //        ReservationStatusName = h.ReservationStatus.Name,
                //        ChangedAt = h.ChangedAt,
                //        ChangedBy = h.ChangedBy,
                //        Note = h.Note
                //    }).ToList()
            };
        }
    }
}
