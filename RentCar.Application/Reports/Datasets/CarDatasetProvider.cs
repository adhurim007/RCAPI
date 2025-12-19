using Microsoft.EntityFrameworkCore;
using RentCar.Application.Reports.Abstractions;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Datasets
{
    public class CarDatasetProvider : IReportDatasetProvider
    {
        private readonly RentCarDbContext _context;

        public CarDatasetProvider(RentCarDbContext context)
        {
            _context = context;
        }

        public string DatasetName => "Car";

        public IReadOnlyCollection<string> SupportedReportCodes =>
            new[] { "RESERVATION_CONTRACT" };

        public DataTable Build(IDictionary<string, object?> parameters)
        {
            if (!parameters.TryGetValue("ReservationId", out var idObj)
                || idObj is not int reservationId)
                throw new ArgumentException("ReservationId is required");

            var car = _context.Reservations
                .AsNoTracking()
                .Where(r => r.Id == reservationId)
                .Select(r => r.Car)
                .Select(c => new
                {
                    c.CarBrand.Name,
                    //c.CarModel.Name.ToString(),
                    c.LicensePlate,
                    c.DailyPrice
                })
                .FirstOrDefault();

            if (car == null)
                throw new Exception("Car not found for reservation");

            var table = CreateTableSchema();

            table.Rows.Add(
                car.Name, 
                car.LicensePlate,
                car.DailyPrice
            );

            return table;
        }

        private static DataTable CreateTableSchema()
        {
            var table = new DataTable("Car");

            table.Columns.Add("Name", typeof(string)); 
            table.Columns.Add("LicensePlate", typeof(string));
            table.Columns.Add("DailyPrice", typeof(int));

            return table;
        }
    }
}
