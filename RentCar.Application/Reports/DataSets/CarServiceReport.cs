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
    public sealed class CarServicesReport : IReport
    {
        private readonly RentCarDbContext _context;
        public string Code => "CAR_SERVICES_REPORT";

        public CarServicesReport(RentCarDbContext context) => _context = context;

        public async Task<DataSet> BuildDataSetAsync(IDictionary<string, object?> parameters, CancellationToken ct)
        {
            if (!parameters.TryGetValue("CarId", out var idObj) || idObj is not int carId)
                throw new ArgumentException("CarId parameter is required");

            var ds = new DataSet("CarServicesReport");

            ds.Tables.Add(await BuildCar(carId, ct));
            ds.Tables.Add(await BuildServices(carId, ct));

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

        private async Task<DataTable> BuildServices(int carId, CancellationToken ct)
        {
            var items = await _context.CarServices
                .Where(x => x.CarId == carId)
                .OrderByDescending(x => x.ServiceDate)
                .Select(x => new
                {
                    x.Id,
                    x.ServiceType,
                    x.ServiceDate,
                    x.Mileage,
                    x.Cost,
                    x.ServiceCenter,
                    x.NextServiceDate,
                    x.NextServiceMileage,
                    x.Notes,
                    x.CreatedAt
                })
                .ToListAsync(ct);

            var t = new DataTable("CarServices");
            t.Columns.Add("Id", typeof(int));
            t.Columns.Add("ServiceType", typeof(int));
            t.Columns.Add("ServiceDate", typeof(DateTime));
            t.Columns.Add("Mileage", typeof(int));
            t.Columns.Add("Cost", typeof(decimal));
            t.Columns.Add("ServiceCenter");
            t.Columns.Add("NextServiceDate", typeof(DateTime));
            t.Columns.Add("NextServiceMileage", typeof(int));
            t.Columns.Add("Notes");
            t.Columns.Add("CreatedAt", typeof(DateTime));

            foreach (var s in items)
            {
                t.Rows.Add(
                    s.Id,
                    s.ServiceType,
                    s.ServiceDate,
                    s.Mileage ?? 0,
                    s.Cost,
                    s.ServiceCenter ?? "",
                    s.NextServiceDate ?? (object)DBNull.Value,
                    s.NextServiceMileage ?? 0,
                    s.Notes ?? "",
                    s.CreatedAt
                );
            }

            return t;
        }
    }
}