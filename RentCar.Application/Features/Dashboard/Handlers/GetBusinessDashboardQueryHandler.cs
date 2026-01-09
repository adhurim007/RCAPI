using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Dashboard;
using RentCar.Application.Features.Dashboard.Queries;
using RentCar.Application.MultiTenancy;
using RentCar.Domain.Enums;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Dashboard.Handlers
{
    public class GetBusinessDashboardQueryHandler : IRequestHandler<GetBusinessDashboardQuery, DashboardSummaryDto>
    {
        private readonly RentCarDbContext _context;
        private readonly ITenantProvider _tenantProvider; // nëse e ke

        public GetBusinessDashboardQueryHandler(RentCarDbContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<DashboardSummaryDto> Handle(GetBusinessDashboardQuery request, CancellationToken cancellationToken)
        {
            // merre businessId prej tenant/claim
            var businessId = 1; // Guid

            var now = DateTime.UtcNow;
            var from = new DateTime(now.Year, now.Month, 1).AddMonths(-11); // 12 muaj mbrapa (fillim muaji)

            // ================= CARDS =================
            var totalReservationsTask = _context.Reservations
                .AsNoTracking()
                .CountAsync(r => r.BusinessId == businessId, cancellationToken);

            var reservationsThisMonthTask = _context.Reservations
                .AsNoTracking()
                .CountAsync(r => r.BusinessId == businessId
                              && r.CreatedAt.Year == now.Year
                              && r.CreatedAt.Month == now.Month, cancellationToken);

            var totalCarsTask = _context.Cars
                .AsNoTracking()
                .CountAsync(c => c.BusinessId == businessId, cancellationToken);

            // Nëse klientët i ruan si tabelë Client me BusinessId:
            //var totalClientsTask = _context.Customer
            //    .AsNoTracking()
            //    .CountAsync(c => c. == businessId, cancellationToken);

            var incomeThisMonthTask = _context.Reservations
                .AsNoTracking()
                .Where(r => r.BusinessId == businessId
                         && r.CreatedAt.Year == now.Year
                         && r.CreatedAt.Month == now.Month
                         && r.PaymentStatus == PaymentStatus.Paid) // përshtate
                .SumAsync(r => (decimal?)r.TotalPrice, cancellationToken);

            var pendingReservationsTask = _context.Reservations
                .AsNoTracking()
                .CountAsync(r => r.BusinessId == businessId
                              && r.PaymentStatus == PaymentStatus.Pending, cancellationToken);

            await Task.WhenAll(
                totalReservationsTask,
                reservationsThisMonthTask,
                totalCarsTask,
                //totalClientsTask,
                incomeThisMonthTask,
                pendingReservationsTask
            );

            // ================= CHARTS =================
            // Reservations per month
            var reservationsGrouped = await _context.Reservations
                .AsNoTracking()
                .Where(r => r.BusinessId == businessId && r.CreatedAt >= from)
                .GroupBy(r => new { r.CreatedAt.Year, r.CreatedAt.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync(cancellationToken);

            // Income per month
            var incomeGrouped = await _context.Reservations
                .AsNoTracking()
                .Where(r => r.BusinessId == businessId && r.CreatedAt >= from && r.PaymentStatus == PaymentStatus.Paid)
                .GroupBy(r => new { r.CreatedAt.Year, r.CreatedAt.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Sum = g.Sum(x => (decimal?)x.TotalPrice) ?? 0m })
                .ToListAsync(cancellationToken);

            // mbush 12 muaj edhe kur s’ka data (0)
            var months = Enumerable.Range(0, 12)
                .Select(i => from.AddMonths(i))
                .ToList();

            List<MonthlyPointDto> reservationsPerMonth = months.Select(d =>
            {
                var hit = reservationsGrouped.FirstOrDefault(x => x.Year == d.Year && x.Month == d.Month);
                return new MonthlyPointDto
                {
                    Year = d.Year,
                    Month = d.Month,
                    Label = d.ToString("MMM yyyy"),
                    Value = hit?.Count ?? 0
                };
            }).ToList();

            List<MonthlyPointDto> incomePerMonth = months.Select(d =>
            {
                var hit = incomeGrouped.FirstOrDefault(x => x.Year == d.Year && x.Month == d.Month);
                return new MonthlyPointDto
                {
                    Year = d.Year,
                    Month = d.Month,
                    Label = d.ToString("MMM yyyy"),
                    Value = hit?.Sum ?? 0m
                };
            }).ToList();

            return new DashboardSummaryDto
            {
                Cards = new DashboardCardsDto
                {
                    TotalReservations = totalReservationsTask.Result,
                    ReservationsThisMonth = reservationsThisMonthTask.Result,
                    TotalCars = totalCarsTask.Result,
                    //TotalClients = totalClientsTask.Result,
                    IncomeThisMonth = incomeThisMonthTask.Result ?? 0m,
                    PendingReservations = pendingReservationsTask.Result
                },
                ReservationsPerMonth = reservationsPerMonth,
                IncomePerMonth = incomePerMonth
            };
        }
    }
}
