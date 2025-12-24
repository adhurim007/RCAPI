using MediatR;
 

namespace RentCar.Application.Reports.Queries
{
    using MediatR;
    using System.Collections.Generic;

    namespace RentCar.Application.Reports.Queries
    {
        public sealed class GenerateReportQuery : IRequest<byte[]>
        {
            public string ReportCode { get; }
            public IDictionary<string, object?> Parameters { get; }

            public GenerateReportQuery(
                string reportCode,
                IDictionary<string, object?> parameters)
            {
                ReportCode = reportCode;
                Parameters = parameters;
            }
        }
    }

}
