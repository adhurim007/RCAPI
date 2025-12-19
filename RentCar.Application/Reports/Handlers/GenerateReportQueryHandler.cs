using MediatR;
using RentCar.Application.Reports.Abstractions;
using RentCar.Application.Reports.Core;
using RentCar.Application.Reports.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Handlers
{
    public class GenerateReportQueryHandler
   : IRequestHandler<GenerateReportQuery, ReportResult>
    {
        private readonly IReportRegistry _registry;

        public GenerateReportQueryHandler(IReportRegistry registry)
        {
            _registry = registry;
        }

        public async Task<ReportResult> Handle(
            GenerateReportQuery request,
            CancellationToken cancellationToken)
        {
            var builder = _registry.GetBuilder(request.Request.ReportCode);

            var data = await builder.BuildAsync(
                request.Request.Parameters,
                cancellationToken
            );

            return new ReportResult
            {
                Data = data
            };
        }
    }


}
