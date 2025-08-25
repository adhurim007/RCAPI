using Microsoft.Reporting.NETCore;
using System.Data;
using System.IO;

namespace RentCar.Application.Reports
{
    public class ReportGenerator
    {
        public byte[] GenerateReport(string reportPath, Dictionary<string, DataTable> datasets)
        {
            LocalReport report = new LocalReport();
            report.ReportPath = reportPath;

            foreach (var dataset in datasets)
            {
                report.DataSources.Add(new ReportDataSource(dataset.Key, dataset.Value));
            }

            byte[] pdfBytes = report.Render("PDF");

            return pdfBytes;
        }
    }
}
