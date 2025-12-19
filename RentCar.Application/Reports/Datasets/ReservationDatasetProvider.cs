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
    public class ReservationDatasetProvider : IReportDatasetProvider
    {
        private readonly RentCarDbContext _context;

        public ReservationDatasetProvider(RentCarDbContext context)
        {
            _context = context;
        }

        public string DatasetName => "Reservation";

        public IReadOnlyCollection<string> SupportedReportCodes =>
            new[] { "RESERVATION_CONTRACT" };

        public DataTable Build(IDictionary<string, object?> parameters)
        {
            if (!parameters.TryGetValue("ReservationId", out var idObj)
                || idObj is not int reservationId)
                throw new ArgumentException("ReservationId is required");

            var reservation = _context.Reservations
                .AsNoTracking()
                .Where(r => r.Id == reservationId)
                .Select(r => new
                {
                    r.Id,
                    r.PickupDate,
                    r.DropoffDate,
                    TotalCost = r.TotalPrice,
                    r.CreatedAt
                })
                .FirstOrDefault();

            if (reservation == null)
                throw new Exception($"Reservation with ID {reservationId} not found");

            var table = CreateTableSchema();

            table.Rows.Add(
                reservation.Id,
                reservation.PickupDate,
                reservation.DropoffDate,
                reservation.TotalCost,
                reservation.CreatedAt
            );

            return table;
        }

        private static DataTable CreateTableSchema()
        {
            var table = new DataTable("Reservation");

            table.Columns.Add("ReservationId", typeof(int));
            table.Columns.Add("PickupDate", typeof(DateTime));
            table.Columns.Add("DropoffDate", typeof(DateTime));
            table.Columns.Add("TotalCost", typeof(decimal));
            table.Columns.Add("CreatedAt", typeof(DateTime));

            return table;
        }
    }
}
