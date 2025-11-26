using Microsoft.EntityFrameworkCore;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports
{
    public class ReservationReportService
    {
        private readonly RentCarDbContext _context;
        private readonly ReportGenerator _reportGenerator;

        public ReservationReportService(RentCarDbContext context, ReportGenerator reportGenerator)
        {
            _context = context;
            _reportGenerator = reportGenerator;
        }

        //public async Task<byte[]> GenerateReservationListReport(DateTime from, DateTime to, int? businessId = null)
        //{
        //    var query = _context.Reservations
        //        .Include(r => r.Car).ThenInclude(c => c.CarModel)
        //        .Include(r => r.Client)
        //        .Include(r => r.Business)
        //        .Include(r => r.ReservationStatus)
        //        .Where(r => r.StartDate >= from && r.EndDate <= to);

        //    if (businessId.HasValue)
        //        query = query.Where(r => r.BusinessId == businessId.Value);

        //    var list = await query.ToListAsync();

        //    DataTable dt = new DataTable("Reservations");
        //    dt.Columns.Add("ReservationId", typeof(int));
        //    dt.Columns.Add("BusinessName", typeof(string));
        //    dt.Columns.Add("ClientName", typeof(string));
        //    dt.Columns.Add("CarModel", typeof(string));
        //    dt.Columns.Add("StartDate", typeof(DateTime));
        //    dt.Columns.Add("EndDate", typeof(DateTime));
        //    dt.Columns.Add("TotalPrice", typeof(decimal));
        //    dt.Columns.Add("Status", typeof(string));

        //    foreach (var r in list)
        //    {
        //        dt.Rows.Add(r.Id,
        //                    r.Business?.CompanyName,
        //                    $"{r.Client?.FirstName} {r.Client?.LastName}".Trim(),
        //                    r.Car?.CarModel?.Name,
        //                    r.StartDate,
        //                    r.EndDate,
        //                    r.TotalPrice,
        //                    r.ReservationStatus?.Name);
        //    }

        //    return _reportGenerator.GenerateReport("Reports/ReservationList.rdl", new Dictionary<string, DataTable> {
        //        { "Reservations", dt }
        //    });
        //}

        //public async Task<byte[]> GenerateIncomeReport(DateTime from, DateTime to)
        //{
        //    var query = await _context.Payments
        //        .Include(p => p.Reservation).ThenInclude(r => r.Business)
        //        .Where(p => p.PaidAt >= from && p.PaidAt <= to)
        //        .GroupBy(p => new { p.Reservation.Business.CompanyName, Month = p.PaidAt.Month, Year = p.PaidAt.Year })
        //        .Select(g => new
        //        {
        //            BusinessName = g.Key.CompanyName,
        //            Period = g.Key.Month + "/" + g.Key.Year,
        //            TotalIncome = g.Sum(x => x.Amount)
        //        })
        //        .ToListAsync();

        //    DataTable dt = new DataTable("Income");
        //    dt.Columns.Add("BusinessName", typeof(string));
        //    dt.Columns.Add("Period", typeof(string));
        //    dt.Columns.Add("TotalIncome", typeof(decimal));

        //    foreach (var item in query)
        //        dt.Rows.Add(item.BusinessName, item.Period, item.TotalIncome);

        //    return _reportGenerator.GenerateReport("Reports/IncomeReport.rdl", new Dictionary<string, DataTable> {
        //        { "Income", dt }
        //    });
        //}

        //public async Task<byte[]> GeneratePendingReservationsReport()
        //{
        //    var pending = await _context.Reservations
        //        .Include(r => r.Car).ThenInclude(c => c.CarModel)
        //        .Include(r => r.Client)
        //        .Include(r => r.Business)
        //        .Include(r => r.ReservationStatus)
        //        .Where(r => r.ReservationStatus.Name == "Pending" || r.Payment == null)
        //        .ToListAsync();

        //    DataTable dt = new DataTable("PendingReservations");
        //    dt.Columns.Add("ReservationId", typeof(int));
        //    dt.Columns.Add("BusinessName", typeof(string));
        //    dt.Columns.Add("ClientName", typeof(string));
        //    dt.Columns.Add("CarModel", typeof(string));
        //    dt.Columns.Add("StartDate", typeof(DateTime));
        //    dt.Columns.Add("EndDate", typeof(DateTime));
        //    dt.Columns.Add("Status", typeof(string));

        //    foreach (var r in pending)
        //    {
        //        dt.Rows.Add(r.Id,
        //                    r.Business?.CompanyName,
        //                    $"{r.Client?.FirstName} {r.Client?.LastName}".Trim(),
        //                    r.Car?.CarModel?.Name,
        //                    r.StartDate,
        //                    r.EndDate,
        //                    r.ReservationStatus?.Name);
        //    }

        //    return _reportGenerator.GenerateReport("Reports/PendingReservations.rdl", new Dictionary<string, DataTable> {
        //        { "PendingReservations", dt }
        //    });
        //}
    }
}
