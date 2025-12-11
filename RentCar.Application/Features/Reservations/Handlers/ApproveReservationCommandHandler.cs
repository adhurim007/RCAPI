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
    public class ApproveReservationCommandHandler : IRequestHandler<ApproveReservationCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public ApproveReservationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ApproveReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return false;

            
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
