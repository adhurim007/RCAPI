using Microsoft.EntityFrameworkCore;
using RentCar.Application.Reports.Abstractions;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Contracts
{
    public sealed class ReservationContractReport : IReport
    {
        private readonly RentCarDbContext _context;

        public string Code => "RESERVATION_CONTRACT";

        public ReservationContractReport(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<DataSet> BuildDataSetAsync(
            IDictionary<string, object?> parameters,
            CancellationToken ct)
        {
            if (!parameters.TryGetValue("ReservationId", out var idObj) || idObj is not int reservationId)
                throw new ArgumentException("ReservationId parameter is required");

            var ds = new DataSet("ReservationContract");

            ds.Tables.Add(await BuildReservation(reservationId, ct));
            ds.Tables.Add(await BuildBusiness(reservationId, ct));
            ds.Tables.Add(await BuildClient(reservationId, ct));
            ds.Tables.Add(await BuildCar(reservationId, ct));

            return ds;
        }
         
        private async Task<DataTable> BuildReservation(int reservationId, CancellationToken ct)
        {
            var r = await _context.Reservations
                .FirstAsync(x => x.Id == reservationId, ct);

            var t = new DataTable("Reservation");
            t.Columns.Add("ReservationId", typeof(int));
            t.Columns.Add("PickupDate", typeof(DateTime));
            t.Columns.Add("DropoffDate", typeof(DateTime));
            t.Columns.Add("TotalCost", typeof(decimal));
            t.Columns.Add("CreatedAt", typeof(DateTime));

            t.Rows.Add(
                r.Id,
                r.PickupDate,
                r.DropoffDate,
                r.TotalPrice,
                r.CreatedAt
            );

            return t;
        }
 
        private async Task<DataTable> BuildBusiness(int reservationId, CancellationToken ct)
        {
            var b = await _context.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => r.Business)
                .FirstAsync(ct);

            var t = new DataTable("Business");
            t.Columns.Add("CompanyName");
            t.Columns.Add("Address");
            t.Columns.Add("ContactPhone");

            t.Rows.Add(
                b.CompanyName,
                b.Address,
                b.ContactPhone
            );

            return t;
        }
         
        private async Task<DataTable> BuildClient(int reservationId, CancellationToken ct)
        {
            var c = await _context.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => r.Customer)
                .FirstAsync(ct);

            var t = new DataTable("Client");
            t.Columns.Add("FullName");
            t.Columns.Add("DocumentNumber");
            t.Columns.Add("PhoneNumber");
            t.Columns.Add("Email");

            t.Rows.Add(
                c.FullName,
                c.DocumentNumber,
                c.PhoneNumber,
                c.Email
            );

            return t;
        }
         
        private async Task<DataTable> BuildCar(int reservationId, CancellationToken ct)
        {
            var car = await _context.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => r.Car)
                .FirstAsync(ct);

            var t = new DataTable("Car");
            t.Columns.Add("Name");
            t.Columns.Add("LicensePlate");
            t.Columns.Add("DailyPrice", typeof(decimal));

            t.Rows.Add(
                    car?.CarBrand?.Name ?? string.Empty,
                    car?.LicensePlate ?? string.Empty,
                    car?.DailyPrice ?? 0m
            );

            return t;
        }
    }
}
