using Microsoft.EntityFrameworkCore;
using RentCar.Application.Reports.Abstractions;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.DataSets
{
    public sealed class VehicleDamageReport : IReport
    {
        private readonly RentCarDbContext _context;
        public string Code => "VEHICLE_DAMAGE_REPORT";

        public VehicleDamageReport(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<DataSet> BuildDataSetAsync(
            IDictionary<string, object?> parameters,
            CancellationToken ct)
        {
            if (!parameters.TryGetValue("ReservationId", out var idObj) || idObj is not int reservationId)
                throw new ArgumentException("ReservationId parameter is required");

            var ds = new DataSet("VehicleDamageInspection");

            ds.Tables.Add(await BuildReservation(reservationId, ct));
            ds.Tables.Add(await BuildBusiness(reservationId, ct));
            ds.Tables.Add(await BuildClient(reservationId, ct));
            ds.Tables.Add(await BuildCar(reservationId, ct));
            ds.Tables.Add(await BuildDamages(reservationId, ct));
            ds.Tables.Add(await BuildDamageSummary(reservationId, ct)); // opsionale (totali i kostos)

            return ds;
        }

        private async Task<DataTable> BuildReservation(int reservationId, CancellationToken ct)
        {
            var r = await _context.Reservations
                .Where(x => x.Id == reservationId)
                .Select(x => new
                {
                    x.Id,
                    x.ReservationNumber,
                    x.PickupDate,
                    x.DropoffDate,
                    x.TotalDays,
                    x.TotalPrice,
                    x.CreatedAt
                })
                .FirstOrDefaultAsync(ct);

            if (r == null)
                throw new ArgumentException($"Reservation with Id={reservationId} was not found");

            var t = new DataTable("Reservation");
            t.Columns.Add("ReservationId", typeof(int));
            t.Columns.Add("ReservationNumber");
            t.Columns.Add("PickupDate", typeof(DateTime));
            t.Columns.Add("DropoffDate", typeof(DateTime));
            t.Columns.Add("TotalDays", typeof(int));
            t.Columns.Add("TotalPrice", typeof(decimal));
            t.Columns.Add("CreatedAt", typeof(DateTime));

            t.Rows.Add(r.Id, r.ReservationNumber, r.PickupDate, r.DropoffDate, r.TotalDays, r.TotalPrice, r.CreatedAt);
            return t;
        }


        private async Task<DataTable> BuildBusiness(int reservationId, CancellationToken ct)
        {
            var b = await _context.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => new
                {
                    r.Business.CompanyName,
                    r.Business.Address,
                    r.Business.ContactPhone
                })
                .FirstAsync(ct);

            var t = new DataTable("Business");
            t.Columns.Add("CompanyName");
            t.Columns.Add("Address");
            t.Columns.Add("ContactPhone");

            t.Rows.Add(b.CompanyName, b.Address, b.ContactPhone);
            return t;
        }

        private async Task<DataTable> BuildClient(int reservationId, CancellationToken ct)
        {
            var c = await _context.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => new
                {
                    r.Customer.FullName,
                    r.Customer.DocumentNumber,
                    r.Customer.PhoneNumber,
                    r.Customer.Email
                })
                .FirstAsync(ct);

            var t = new DataTable("Client");
            t.Columns.Add("FullName");
            t.Columns.Add("DocumentNumber");
            t.Columns.Add("PhoneNumber");
            t.Columns.Add("Email");

            t.Rows.Add(c.FullName, c.DocumentNumber, c.PhoneNumber, c.Email);
            return t;
        }

        private async Task<DataTable> BuildCar(int reservationId, CancellationToken ct)
        {
            var car = await _context.Reservations
                .Where(r => r.Id == reservationId)
                .Select(r => new
                {
                    Brand = r.Car.CarBrand.Name,
                    Model = r.Car.CarModel.Name,
                    r.Car.LicensePlate,
                    r.Car.DailyPrice
                })
                .FirstAsync(ct);

            var t = new DataTable("Car");
            t.Columns.Add("Brand");
            t.Columns.Add("Model");
            t.Columns.Add("LicensePlate");
            t.Columns.Add("DailyPrice", typeof(decimal));

            t.Rows.Add(car.Brand ?? "", car.Model ?? "", car.LicensePlate ?? "", car.DailyPrice);
            return t;
        }

        private async Task<DataTable> BuildDamages(int reservationId, CancellationToken ct)
        {
            var damages = await _context.VehicleDamage
                .Where(d => d.ReservationId == reservationId)
                .OrderByDescending(d => d.Id)
                .Select(d => new
                {
                    d.Id,
                    d.DamageType,
                    d.Description,
                    d.EstimatedCost,
                    d.Status,
                    d.CreatedAt
                })
                .ToListAsync(ct);

            var t = new DataTable("Damages");
            t.Columns.Add("Id", typeof(int));
            t.Columns.Add("DamageType");
            t.Columns.Add("Description");
            t.Columns.Add("EstimatedCost", typeof(decimal));
            t.Columns.Add("Status", typeof(int));
            t.Columns.Add("CreatedAt", typeof(DateTime));

            foreach (var d in damages)
            {
                t.Rows.Add(
                    d.Id,
                    d.DamageType ?? "",
                    d.Description ?? "",
                    d.EstimatedCost,
                    d.Status,
                    d.CreatedAt
                );
            }

            return t;
        }

        private async Task<DataTable> BuildDamageSummary(int reservationId, CancellationToken ct)
        {
            var sum = await _context.VehicleDamage
                .Where(d => d.ReservationId == reservationId)
                .Select(d => (decimal?)d.EstimatedCost)
                .SumAsync(ct) ?? 0m;

            var t = new DataTable("DamageSummary");
            t.Columns.Add("TotalEstimatedCost", typeof(decimal));
            t.Rows.Add(sum);
            return t;
        }
    }
}
