using Microsoft.EntityFrameworkCore;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace RentCar.Application.Reports
{
    public class ReservationReportService : IReservationReportService
    {
        private readonly RentCarDbContext _context;
        private readonly ReportGenerator _reportGenerator;

        public ReservationReportService(RentCarDbContext context, ReportGenerator reportGenerator)
        {
            _context = context;
            _reportGenerator = reportGenerator;
        }

        public async Task<byte[]> GenerateReservationListReport(DateTime from, DateTime to, int? businessId = null)
        {
            var query = _context.Reservations
                .Include(r => r.Car).ThenInclude(c => c.CarModel)
                .Include(r => r.Business)
                .Include(r => r.ReservationStatus)
                .Where(r => r.PickupDate >= from && r.DropoffDate <= to);

            if (businessId.HasValue)
                query = query.Where(r => r.BusinessId == businessId.Value);

            var list = await query.ToListAsync();

            DataTable dt = new("Reservations");
            dt.Columns.Add("ReservationId", typeof(int));
            dt.Columns.Add("BusinessName", typeof(string));
            dt.Columns.Add("CarModel", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("TotalPrice", typeof(decimal));
            dt.Columns.Add("Status", typeof(string));

            foreach (var r in list)
            {
                dt.Rows.Add(
                    r.Id,
                    r.Business?.CompanyName,
                    r.Car?.CarModel?.Name,
                    r.PickupDate,
                    r.DropoffDate,
                    r.TotalPrice,
                    r.ReservationStatus?.Name
                );
            }

            return _reportGenerator.GenerateReport(
                "Reports/ReservationList.rdl",
                new Dictionary<string, DataTable> { { "Reservations", dt } }
            );
        }

        public async Task<byte[]> GenerateIncomeReport(DateTime from, DateTime to)
        {
            var items = await _context.Payments
                .Include(p => p.Reservation).ThenInclude(r => r.Business)
                .Where(p => p.CreatedAt >= from && p.CreatedAt <= to)
                .GroupBy(p => new { p.Reservation.Business.CompanyName, p.CreatedAt.Month, p.CreatedAt.Year })
                .Select(g => new
                {
                    BusinessName = g.Key.CompanyName,
                    Period = $"{g.Key.Month}/{g.Key.Year}",
                    TotalIncome = g.Sum(x => x.Amount)
                })
                .ToListAsync();

            DataTable dt = new("Income");
            dt.Columns.Add("BusinessName", typeof(string));
            dt.Columns.Add("Period", typeof(string));
            dt.Columns.Add("TotalIncome", typeof(decimal));

            foreach (var item in items)
                dt.Rows.Add(item.BusinessName, item.Period, item.TotalIncome);

            return _reportGenerator.GenerateReport(
                "Reports/IncomeReport.rdl",
                new Dictionary<string, DataTable> { { "Income", dt } }
            );
        }

        public async Task<byte[]> GeneratePendingReservationsReport()
        {
            var list = await _context.Reservations
                .Include(r => r.Car).ThenInclude(c => c.CarModel)
                .Include(r => r.Business)
                .Include(r => r.ReservationStatus)
                .Where(r => r.ReservationStatus.Name == "Pending")
                .ToListAsync();

            DataTable dt = new("PendingReservations");
            dt.Columns.Add("ReservationId", typeof(int));
            dt.Columns.Add("BusinessName", typeof(string));
            dt.Columns.Add("CarModel", typeof(string));
            dt.Columns.Add("StartDate", typeof(DateTime));
            dt.Columns.Add("EndDate", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));

            foreach (var r in list)
            {
                dt.Rows.Add(
                    r.Id,
                    r.Business?.CompanyName,
                    r.Car?.CarModel?.Name,
                    r.PickupDate,
                    r.DropoffDate,
                    r.ReservationStatus?.Name
                );
            }

            return _reportGenerator.GenerateReport(
                "Reports/PendingReservations.rdl",
                new Dictionary<string, DataTable> { { "PendingReservations", dt } }
            );
        }
    }
}
