using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Dashboard;
using RentCar.Application.Features.Dashboard.Queries;
using RentCar.Application.MultiTenancy;
using RentCar.Domain.Entities;
using RentCar.Domain.Enums;
using RentCar.Persistence;

namespace RentCar.Application.Features.Dashboard.Handlers
{
    public class GetBusinessDashboardQueryHandler
        : IRequestHandler<GetBusinessDashboardQuery, DashboardSummaryDto>
    {
        private readonly RentCarDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public GetBusinessDashboardQueryHandler(RentCarDbContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<DashboardSummaryDto> Handle(GetBusinessDashboardQuery request, CancellationToken cancellationToken)
        {
            // ✅ 1) BusinessId (TEST MODE)
            // Kur ta rregullon JWT, ktheje në: var businessId = _tenantProvider.GetBusinessId();
            var businessId = await _context.Businesses
                .AsNoTracking()
                .Select(x => x.Id) // Guid
                .FirstOrDefaultAsync(cancellationToken);

            //if (businessId == Guid.Empty)
            //{
            //    // s’ka asnjë biznes në DB
            //    return new DashboardSummaryDto();
            //}

            var now = DateTime.UtcNow;
            var from = new DateTime(now.Year, now.Month, 1).AddMonths(-11); // 12 muaj mbrapa

            // ✅ 2) CARDS (një nga një - pa Task.WhenAll)
            var totalReservations = await _context.Reservations
                .AsNoTracking()
                .CountAsync(r => r.BusinessId == businessId, cancellationToken);

            var reservationsThisMonth = await _context.Reservations
                .AsNoTracking()
                .CountAsync(r => r.BusinessId == businessId
                    && r.CreatedAt.Year == now.Year
                    && r.CreatedAt.Month == now.Month, cancellationToken);

            var totalCars = await _context.Cars
                .AsNoTracking()
                .CountAsync(c => c.BusinessId == businessId, cancellationToken);

            // ✅ Nëse s’ke Clients tabelë, komentoje këtë:
            var totalClients = await _context.Reservations
                .AsNoTracking()
                .Where(r => r.Car.BusinessId == businessId && r.CustomerId != null)
                .Select(r => r.CustomerId)
                .Distinct()
                .CountAsync(cancellationToken);

            var incomeThisMonth = await _context.Reservations
                .AsNoTracking()
                .Where(r => r.BusinessId == businessId
                    && r.CreatedAt.Year == now.Year
                    && r.CreatedAt.Month == now.Month
                    && r.PaymentStatus == PaymentStatus.Paid)
                .SumAsync(r => (decimal?)r.TotalPrice, cancellationToken) ?? 0m;

            // ✅ Pending: përdor ReservationStatus (jo PaymentStatus) - zgjidh njërën
            var pendingReservations = await _context.Reservations
                .AsNoTracking()
                .CountAsync(r => r.BusinessId == businessId
                    && r.PaymentStatus == PaymentStatus.Pending, cancellationToken);

            // ✅ 3) CHARTS (12 muajt e fundit)
            var reservationsGrouped = await _context.Reservations
                .AsNoTracking()
                .Where(r => r.BusinessId == businessId && r.CreatedAt >= from)
                .GroupBy(r => new { r.CreatedAt.Year, r.CreatedAt.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Count = g.Count() })
                .ToListAsync(cancellationToken);

            var incomeGrouped = await _context.Reservations
                .AsNoTracking()
                .Where(r => r.BusinessId == businessId
                    && r.CreatedAt >= from
                    && r.PaymentStatus == PaymentStatus.Paid)
                .GroupBy(r => new { r.CreatedAt.Year, r.CreatedAt.Month })
                .Select(g => new { g.Key.Year, g.Key.Month, Sum = g.Sum(x => (decimal?)x.TotalPrice) ?? 0m })
                .ToListAsync(cancellationToken);

            // ✅ 4) Mbush 12 muaj edhe kur s’ka data
            var months = Enumerable.Range(0, 12)
                .Select(i => from.AddMonths(i))
                .ToList();

            var reservationsPerMonth = months.Select(d =>
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

            var incomePerMonth = months.Select(d =>
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

            // ✅ 5) RETURN
            return new DashboardSummaryDto
            {
                Cards = new DashboardCardsDto
                {
                    TotalReservations = totalReservations,
                    ReservationsThisMonth = reservationsThisMonth,
                    TotalCars = totalCars,
                    TotalClients = totalClients,
                    IncomeThisMonth = incomeThisMonth,
                    PendingReservations = pendingReservations
                },
                ReservationsPerMonth = reservationsPerMonth,
                IncomePerMonth = incomePerMonth
            };
        }
    }
}
