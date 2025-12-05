using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports
{
    public interface IReservationReportService
    {
        Task<byte[]> GenerateReservationListReport(DateTime from, DateTime to, int? businessId = null);
        Task<byte[]> GenerateIncomeReport(DateTime from, DateTime to);
        Task<byte[]> GeneratePendingReservationsReport();
    }
}
