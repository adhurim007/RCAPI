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
    public class GetAdminDashboardQueryHandler : IRequestHandler<GetAdminDashboardQuery, AdminDashboardDto>
    {
        private readonly RentCarDbContext _context;

        public GetAdminDashboardQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<AdminDashboardDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            return new AdminDashboardDto
            {
                TotalUsers = await _context.Users.CountAsync(cancellationToken),
                TotalBusinesses = await _context.Businesses.CountAsync(cancellationToken),
                TotalClients = await _context.Clients.CountAsync(cancellationToken),
                ActiveReservations = await _context.Reservations.CountAsync(r => r.ReservationStatusId == 2, cancellationToken), // 2 = Active
                CanceledReservations = await _context.Reservations.CountAsync(r => r.ReservationStatusId == 3, cancellationToken), // 3 = Canceled
                PendingReservations = await _context.Reservations.CountAsync(r => r.ReservationStatusId == 1, cancellationToken), // 1 = Pending
                TotalRevenue = await _context.Payments.SumAsync(p => p.Amount, cancellationToken),
                RecentAuditLogs = await _context.AuditLogs
                    .OrderByDescending(l => l.Timestamp)
                    .Take(10)
                    .Select(l => $"{l.Timestamp:u} - {l.Action} on {l.EntityName} ({l.UserId})")
                    .ToListAsync(cancellationToken),
                RecentReservations = await _context.Reservations
                    .OrderByDescending(r => r.StartDate)
                    .Take(10)
                    .Select(r => $"#{r.Id} - Car:{r.CarId} Client:{r.ClientId} Status:{r.ReservationStatusId}")
                    .ToListAsync(cancellationToken)
            };
        }
    }
}
