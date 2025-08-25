using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Application.MultiTenancy;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class GetReservationsByBusinessIdQueryHandler
        : IRequestHandler<GetReservationsByBusinessIdQuery, List<ReservationDto>>
    {
        private readonly RentCarDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public GetReservationsByBusinessIdQueryHandler(RentCarDbContext context, ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<List<ReservationDto>> Handle(GetReservationsByBusinessIdQuery request, CancellationToken cancellationToken)
        {
            var businessId = _tenantProvider.GetBusinessId();

            if (!_tenantProvider.IsSuperAdmin() && businessId != request.BusinessId)
                throw new UnauthorizedAccessException("Not allowed to view these reservations.");

            return await _context.Reservations
                .Where(r => r.BusinessId == request.BusinessId)
                .Select(r => new ReservationDto
                {
                    Id = r.Id,
                    CarId = r.CarId,
                    ClientId = r.ClientId,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    TotalPrice = r.TotalPrice,
                    //ReservationStatusId = r.ReservationStatusId
                })
                .ToListAsync(cancellationToken);
        }
    }
}
