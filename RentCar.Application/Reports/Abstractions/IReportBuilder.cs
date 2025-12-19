using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Abstractions
{
    public interface IReportBuilder
    {
        string ReportCode { get; }

        Task<object> BuildAsync(
            IDictionary<string, object?> parameters,
            CancellationToken cancellationToken
        );
    }
}
