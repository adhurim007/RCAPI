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
    public class BusinessDatasetProvider : IReportDatasetProvider
    {
        private readonly RentCarDbContext _context;

        public BusinessDatasetProvider(RentCarDbContext context)
        {
            _context = context;
        }

        public string DatasetName => "Business";

        public IReadOnlyCollection<string> SupportedReportCodes =>
            new[] { "RESERVATION_CONTRACT" };

        public DataTable Build(IDictionary<string, object?> parameters)
        {
            if (!parameters.TryGetValue("ReservationId", out var idObj)
                || idObj is not int reservationId)
                throw new ArgumentException("ReservationId is required");

            var business = _context.Reservations
                .AsNoTracking()
                .Where(r => r.Id == reservationId)
                .Select(r => r.Business)
                .Select(b => new
                {
                    b.CompanyName,
                    b.Address,
                    b.ContactPhone,
                    
                })
                .FirstOrDefault();

            if (business == null)
                throw new Exception("Business not found for reservation");

            var table = CreateTableSchema();

            table.Rows.Add(
                business.CompanyName,
                business.Address,
                business.ContactPhone
                
            );

            return table;
        }

        private static DataTable CreateTableSchema()
        {
            var table = new DataTable("Business");

            table.Columns.Add("CompanyName", typeof(string));
            table.Columns.Add("Address", typeof(string));
            table.Columns.Add("ContactPhone", typeof(string)); 

            return table;
        }
    }
}
