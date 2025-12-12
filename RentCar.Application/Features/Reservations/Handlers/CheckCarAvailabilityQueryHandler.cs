using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class CheckCarAvailabilityQueryHandler
        : IRequestHandler<CheckCarAvailabilityQuery, bool>
    {
        private readonly RentCarDbContext _context;

        public CheckCarAvailabilityQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            CheckCarAvailabilityQuery request,
            CancellationToken cancellationToken)
        {
            if (request.From >= request.To)
                return false;

            bool hasConflict = await _context.Reservations
                .AnyAsync(r =>
                    r.CarId == request.CarId &&
                    (!request.ExcludeReservationId.HasValue || r.Id != request.ExcludeReservationId.Value) &&
                    r.PickupDate < request.To &&
                    r.DropoffDate > request.From,
                    cancellationToken
                );
             
            return !hasConflict;
        }
    }
}
