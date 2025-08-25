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
    public class GetIncomePerMonthQueryHandler : IRequestHandler<GetIncomePerMonthQuery, List<MonthlyIncomeDto>>
    {
        private readonly RentCarDbContext _context;

        public GetIncomePerMonthQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<MonthlyIncomeDto>> Handle(GetIncomePerMonthQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Payments.Include(p => p.Reservation).AsQueryable();

            if (request.BusinessId.HasValue)
                query = query.Where(p => p.Reservation.BusinessId == request.BusinessId);

            return await query
                .GroupBy(p => new { p.PaidAt.Year, p.PaidAt.Month })
                .Select(g => new MonthlyIncomeDto
                {
                    Month = $"{g.Key.Year}-{g.Key.Month:D2}",
                    Income = g.Sum(x => x.Amount)
                })
                .OrderBy(x => x.Month)
                .ToListAsync(cancellationToken);
        }
    }
}
