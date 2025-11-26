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
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateReservationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return false;

           // reservation.StartDate = request.StartDate;
           // reservation.EndDate = request.EndDate;
           // reservation.TotalPrice = request.TotalPrice;
           // reservation.ReservationStatusId = request.ReservationStatusId;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
