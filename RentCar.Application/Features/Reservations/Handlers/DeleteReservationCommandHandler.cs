using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class DeleteReservationCommandHandler
        : IRequestHandler<DeleteReservationCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public DeleteReservationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        { 
            var reservation = await _context.Reservations
                .Include(r => r.ExtraServices)
                .Include(r => r.Payments)
                .Include(r => r.Contract)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return false;
             
            if (reservation.ExtraServices != null && reservation.ExtraServices.Any())
                _context.ReservationExtraServices.RemoveRange(reservation.ExtraServices);
             
            if (reservation.Payments != null && reservation.Payments.Any())
                _context.Payments.RemoveRange(reservation.Payments);
             
            if (reservation.Contract != null)
                _context.Contracts.Remove(reservation.Contract);
             
            _context.Reservations.Remove(reservation);
             
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
