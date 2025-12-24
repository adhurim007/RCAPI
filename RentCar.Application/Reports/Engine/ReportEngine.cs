using RentCar.Application.Reports.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Engine
{
    public sealed class ReportEngine
    {
        private readonly IEnumerable<IReport> _reports;
        private readonly IReportRenderer _renderer;

        public ReportEngine(
            IEnumerable<IReport> reports,
            IReportRenderer renderer)
        {
            _reports = reports;
            _renderer = renderer;
        }

        public async Task<byte[]> GeneratePdfAsync(
            string reportCode,
            IDictionary<string, object?> parameters,
            CancellationToken cancellationToken)
        {
            var report = _reports.FirstOrDefault(r => r.Code == reportCode);

            if (report is null)
                throw new InvalidOperationException(
                    $"Report with code '{reportCode}' not registered.");

            var dataSet = await report.BuildDataSetAsync(
                parameters,
                cancellationToken);

            return _renderer.Render(reportCode, dataSet);
        }
    }

}
