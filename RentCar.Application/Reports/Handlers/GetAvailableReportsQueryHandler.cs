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
    public class GetAvailableReportsQueryHandler
    : IRequestHandler<GetAvailableReportsQuery, IReadOnlyCollection<ReportDefinition>>
    {
        private readonly IReportRegistry _registry;

        public GetAvailableReportsQueryHandler(IReportRegistry registry)
        {
            _registry = registry;
        }

        public Task<IReadOnlyCollection<ReportDefinition>> Handle(
            GetAvailableReportsQuery request,
            CancellationToken cancellationToken)
        {
            var reports = _registry
                .GetAll()
                .Select(r => new ReportDefinition
                {
                    Code = r.Code,
                    Name = r.Name,
                    Description = r.Description
                })
                .ToList()
                .AsReadOnly();

            return Task.FromResult<IReadOnlyCollection<ReportDefinition>>(reports);
        }
    }
}
