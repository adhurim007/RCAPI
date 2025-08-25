using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Dashboard;
using RentCar.Application.Features.Dashboard.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Dashboard.Handlers
{
    public class GetReservationsPerMonthQueryHandler : IRequestHandler<GetReservationsPerMonthQuery, List<MonthlyStatDto>>
    {
        private readonly RentCarDbContext _context;

        public GetReservationsPerMonthQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyStatDto>> Handle(GetReservationsPerMonthQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Reservations.AsQueryable();

            if (request.BusinessId.HasValue)
                query = query.Where(r => r.BusinessId == request.BusinessId);

            return await query
                .GroupBy(r => new { r.StartDate.Year, r.StartDate.Month })
                .Select(g => new MonthlyStatDto
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Count = g.Count()
                })
                .OrderBy(x => x.Month)
                .ToListAsync(cancellationToken);
        }
    }
}
