using MediatR;
using RentCar.Application.DTOs.Dashboard;
using RentCar.Application.Features.Dashboard.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Dashboard.Handlers
{
    public class GetAdminDashboardQueryHandler
        : IRequestHandler<GetAdminDashboardQuery, AdminDashboardSummaryDto>
    {
        public Task<AdminDashboardSummaryDto> Handle(GetAdminDashboardQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new AdminDashboardSummaryDto());
        }
    }
}
