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
    public class GetReservationDetailsQueryHandler : IRequestHandler<GetReservationDetailsQuery, ReservationDetailsDto>
    {
        private readonly RentCarDbContext _context;

        public GetReservationDetailsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDetailsDto?> Handle(GetReservationDetailsQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Car).ThenInclude(c => c.CarModel)
                .Include(r => r.Client)
                .Include(r => r.Business)
                .Include(r => r.ReservationStatus)
                .Include(r => r.Payment)
                .Include(r => r.Contract)
                //.Include(r => r.ReservationStatusHistories)
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId, cancellationToken);

            if (reservation == null)
                return null;

            return new ReservationDetailsDto
            {
                Id = reservation.Id,
                CarId = reservation.CarId,
                ClientId = reservation.ClientId,
                BusinessId = reservation.BusinessId,
                CarModel = reservation.Car?.CarModel?.Name ?? string.Empty,
                ClientName = $"{reservation.Client?.FirstName} {reservation.Client?.LastName}".Trim(),
                BusinessName = reservation.Business?.CompanyName,
                Status = reservation.ReservationStatus?.Name,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                TotalPrice = reservation.TotalPrice,
                Payments = reservation.Payment != null
                    ? new List<PaymentDto>
                    {
                        new PaymentDto
                        {
                            Id = reservation.Payment.Id,
                            Amount = reservation.Payment.Amount,
                            PaidAt = reservation.Payment.PaidAt,
                            PaymentMethod = reservation.Payment.PaymentMethod
                        }
                    }
                    : new List<PaymentDto>(),
                Contract = reservation.Contract != null
                    ? new ContractDto
                    {
                        Id = reservation.Contract.Id,
                        FileUrl = reservation.Contract.FileUrl,
                        CreatedAt = reservation.Contract.CreatedAt
                    }
                    : null,
                //StatusHistory = reservation.ReservationStatusHistory?
                //    .Select(h => new ReservationStatusHistoryDto
                //    {
                //        Id = h.Id,
                //        Status = h.ReservationStatus?.Name,
                //        ChangedAt = h.ChangedAt,
                //        ChangedBy = h.ChangedBy,
                //        Note = h.Note
                //    }).ToList() ?? new List<ReservationStatusHistoryDto>()
            };
        }
    }
}
