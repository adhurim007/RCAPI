using RentCar.Application.Reports.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Implementations
{

    public class DynamicReportBuilder : IReportBuilder
    {
        private readonly IEnumerable<IReportDatasetProvider> _providers;

        public DynamicReportBuilder(IEnumerable<IReportDatasetProvider> providers)
        {
            _providers = providers;
        }

        /// <summary>
        /// Builder fallback për të gjitha raportet dinamike
        /// </summary>
        public string ReportCode => "DYNAMIC";

        public Task<object> BuildAsync(
            IDictionary<string, object?> parameters,
            CancellationToken cancellationToken)
        {
            if (!parameters.TryGetValue("ReportCode", out var rcObj)
                || rcObj is not string reportCode)
            {
                throw new ArgumentException("ReportCode parameter is required.");
            }

            var dataSet = new DataSet(reportCode);

            var providersForReport = _providers
                .Where(p => p.SupportedReportCodes.Contains(reportCode));

            foreach (var provider in providersForReport)
            {
                var table = provider.Build(parameters);
                table.TableName = provider.DatasetName; // KRITIKE për SSRS
                dataSet.Tables.Add(table);
            }

            return Task.FromResult<object>(dataSet);
        }
    }


}
