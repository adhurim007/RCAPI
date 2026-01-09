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
    public sealed class CarRegistrationsReport : IReport
    {
        private readonly RentCarDbContext _context;
        public string Code => "CAR_REGISTRATIONS_REPORT";

        public CarRegistrationsReport(RentCarDbContext context) => _context = context;

        public async Task<DataSet> BuildDataSetAsync(IDictionary<string, object?> parameters, CancellationToken ct)
        {
            if (!parameters.TryGetValue("CarId", out var idObj) || idObj is not int carId)
                throw new ArgumentException("CarId parameter is required");

            var ds = new DataSet("CarRegistrationsReport");

            ds.Tables.Add(await BuildCar(carId, ct));
            ds.Tables.Add(await BuildRegistrations(carId, ct));

            return ds;
        }

        private async Task<DataTable> BuildCar(int carId, CancellationToken ct)
        {
            var car = await _context.Cars
                .Where(c => c.Id == carId)
                .Select(c => new
                {
                    c.Id,
                    Brand = c.CarBrand.Name,
                    Model = c.CarModel.Name,
                    c.LicensePlate,
                    c.DailyPrice,
                    c.Color
                })
                .FirstOrDefaultAsync(ct);

            if (car == null)
                throw new ArgumentException($"Car with Id={carId} not found");

            var t = new DataTable("Car");
            t.Columns.Add("CarId", typeof(int));
            t.Columns.Add("Brand");
            t.Columns.Add("Model");
            t.Columns.Add("LicensePlate");
            t.Columns.Add("DailyPrice", typeof(decimal));
            t.Columns.Add("Color");

            t.Rows.Add(car.Id, car.Brand ?? "", car.Model ?? "", car.LicensePlate ?? "", car.DailyPrice, car.Color ?? "");
            return t;
        }

        private async Task<DataTable> BuildRegistrations(int carId, CancellationToken ct)
        {
            var items = await _context.CarRegistrations
                .Where(x => x.CarId == carId)
                .OrderByDescending(x => x.IssuedDate)
                .Select(x => new
                {
                    x.Id,
                    x.RegistrationNumber,
                    x.IssuedDate,
                    x.ExpiryDate,
                    x.Cost,
                    x.InsuranceCompany,
                    x.InsuranceExpiryDate,
                    x.Notes,
                    x.CreatedAt
                })
                .ToListAsync(ct);

            var t = new DataTable("CarRegistrations");
            t.Columns.Add("Id", typeof(int));
            t.Columns.Add("RegistrationNumber");
            t.Columns.Add("IssuedDate", typeof(DateTime));
            t.Columns.Add("ExpiryDate", typeof(DateTime));
            t.Columns.Add("Cost", typeof(decimal));
            t.Columns.Add("InsuranceCompany");
            t.Columns.Add("InsuranceExpiryDate", typeof(DateTime));
            t.Columns.Add("Notes");
            t.Columns.Add("CreatedAt", typeof(DateTime));

            foreach (var r in items)
            {
                t.Rows.Add(
                    r.Id,
                    r.RegistrationNumber ?? "",
                    r.IssuedDate,
                    r.ExpiryDate,
                    r.Cost,
                    r.InsuranceCompany ?? "",
                    r.InsuranceExpiryDate ?? (object)DBNull.Value,
                    r.Notes ?? "",
                    r.CreatedAt
                );
            }

            return t;
        }
    }
}
