using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.ExtraServices.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.ExtraServices.Handlers
{
    public class GetAllExtraServicesQueryHandler : IRequestHandler<GetAllExtraServicesQuery, List<ExtraService>>
    {
        private readonly RentCarDbContext _context;

        public GetAllExtraServicesQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<ExtraService>> Handle(GetAllExtraServicesQuery request, CancellationToken cancellationToken)
        {
            return await _context.ExtraServices
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
