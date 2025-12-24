using Microsoft.Reporting.NETCore;
using System.Data;

namespace RentCar.Api.Reports.Rendering
{
    public static class RdlPdfRenderer
    {
        public static byte[] Render(string rdlPath, DataSet dataSet)
        {
            if (!File.Exists(rdlPath))
                throw new FileNotFoundException("RDL not found", rdlPath);

            using var report = new LocalReport
            {
                ReportPath = rdlPath,
                EnableExternalImages = true
            };

            foreach (DataTable table in dataSet.Tables)
            {
                report.DataSources.Add(
                    new ReportDataSource(table.TableName, table)
                );
            }

            return report.Render("PDF");
        }
    }
}
