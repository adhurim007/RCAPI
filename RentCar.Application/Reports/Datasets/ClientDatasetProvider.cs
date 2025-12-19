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
    public class ClientDatasetProvider : IReportDatasetProvider
    {
        private readonly RentCarDbContext _context;

        public ClientDatasetProvider(RentCarDbContext context)
        {
            _context = context;
        }

        public string DatasetName => "Client";

        public IReadOnlyCollection<string> SupportedReportCodes =>
            new[] { "RESERVATION_CONTRACT" };

        public DataTable Build(IDictionary<string, object?> parameters)
        {
            if (!parameters.TryGetValue("ReservationId", out var idObj)
                || idObj is not int reservationId)
                throw new ArgumentException("ReservationId is required");

            var client = _context.Reservations
                .AsNoTracking()
                .Where(r => r.Id == reservationId)
                .Select(r => r.Customer)
                .Select(c => new
                {
                    c.FullName,
                    c.DocumentNumber,
                    c.PhoneNumber,
                    c.Email
                })
                .FirstOrDefault();

            if (client == null)
                throw new Exception("Client not found for reservation");

            var table = CreateTableSchema();

            table.Rows.Add(
                client.FullName,
                client.DocumentNumber,
                client.PhoneNumber,
                client.Email
            );

            return table;
        }

        private static DataTable CreateTableSchema()
        {
            var table = new DataTable("Client");

            table.Columns.Add("FullName", typeof(string));
            table.Columns.Add("DocumentNumber", typeof(string));
            table.Columns.Add("PhoneNumber", typeof(string));
            table.Columns.Add("Email", typeof(string));

            return table;
        }
    }
}
