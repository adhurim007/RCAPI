using MediatR;
using RentCar.Application.Reports.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Queries
{
    public record GenerateReportQuery(ReportRequest Request) : IRequest<ReportResult>;
}
