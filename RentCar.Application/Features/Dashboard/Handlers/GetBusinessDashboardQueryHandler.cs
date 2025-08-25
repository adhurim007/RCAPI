using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Dashboard;
using RentCar.Application.Features.Dashboard.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Dashboard.Handlers
{
    public class GetBusinessDashboardQueryHandler : IRequestHandler<GetBusinessDashboardQuery, BusinessDashboardDto>
    {
        private readonly RentCarDbContext _context;

        public GetBusinessDashboardQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<BusinessDashboardDto> Handle(GetBusinessDashboardQuery request, CancellationToken cancellationToken)
        {
            var businessId = request.BusinessId;

            return new BusinessDashboardDto
            {
                TotalCars = await _context.Cars.CountAsync(c => c.BusinessId == businessId, cancellationToken),
                AvailableCars = await _context.Cars.CountAsync(c => c.BusinessId == businessId && c.IsAvailable, cancellationToken),
                ActiveReservations = await _context.Reservations.CountAsync(r => r.BusinessId == businessId && r.ReservationStatusId == 2, cancellationToken),
                PendingReservations = await _context.Reservations.CountAsync(r => r.BusinessId == businessId && r.ReservationStatusId == 1, cancellationToken),
                CanceledReservations = await _context.Reservations.CountAsync(r => r.BusinessId == businessId && r.ReservationStatusId == 3, cancellationToken),
                Revenue = await _context.Payments
                    .Where(p => p.Reservation.BusinessId == businessId)
                    .SumAsync(p => p.Amount, cancellationToken),
                RecentReservations = await _context.Reservations
                    .Where(r => r.BusinessId == businessId)
                    .OrderByDescending(r => r.StartDate)
                    .Take(5)
                    .Select(r => $"#{r.Id} - Client:{r.ClientId} Status:{r.ReservationStatusId}")
                    .ToListAsync(cancellationToken),
                Notifications = await _context.Notifications
                    .Where(n => n.UserId == businessId.ToString())
                    .OrderByDescending(n => n.SentAt)
                    .Take(5)
                    .Select(n => $"{n.SentAt:u} - {n.Message}")
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
