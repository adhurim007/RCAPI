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
    public class GetExtraServiceByIdQueryHandler : IRequestHandler<GetExtraServiceByIdQuery, ExtraService>
    {
        private readonly RentCarDbContext _context;

        public GetExtraServiceByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<ExtraService> Handle(GetExtraServiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _context.ExtraServices.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}
