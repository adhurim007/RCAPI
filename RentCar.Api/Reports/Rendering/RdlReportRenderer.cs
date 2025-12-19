using Microsoft.Reporting.NETCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Rendering
{
    public class RdlReportRenderer : IRdlReportRenderer
    {
        public string RenderToPdfAndSave(
            string rdlPath,
            DataSet dataSet,
            string outputDirectory,
            string fileName)
        {
            var report = new LocalReport();

            using var rdlStream = File.OpenRead(rdlPath);
            report.LoadReportDefinition(rdlStream);

            foreach (DataTable table in dataSet.Tables)
            {
                report.DataSources.Add(
                    new ReportDataSource(table.TableName, table)
                );
            }

            var deviceInfo = @"
            <DeviceInfo> <OutputFormat>PDF</OutputFormat> <PageWidth>21cm</PageWidth> <PageHeight>29.7cm</PageHeight> <MarginTop>1.5cm</MarginTop> <MarginLeft>2cm</MarginLeft> <MarginRight>2cm</MarginRight> <MarginBottom>1.5cm</MarginBottom> </DeviceInfo>";

            var pdfBytes = report.Render(
                "PDF",
                deviceInfo,
                out _,
                out _,
                out _,
                out _,
                out _
            );

            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            var fullPath = Path.Combine(outputDirectory, fileName);
            File.WriteAllBytes(fullPath, pdfBytes);

            return fullPath;
        }
    }



}
