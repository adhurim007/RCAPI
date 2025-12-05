using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Application.MultiTenancy;
using RentCar.Persistence;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class DeleteReservationCommandHandler
        : IRequestHandler<DeleteReservationCommand, bool>
    {
        private readonly RentCarDbContext _context;
        private readonly ITenantProvider _tenantProvider;

        public DeleteReservationCommandHandler(
            RentCarDbContext context,
            ITenantProvider tenantProvider)
        {
            _context = context;
            _tenantProvider = tenantProvider;
        }

        public async Task<bool> Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
        {
            // 1. Gjej rezervimin
            var reservation = await _context.Reservations
                .Include(r => r.Payments)
                .Include(r => r.Contract)
                .Include(r => r.ReservationStatusHistories)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return false;

            // 2. Kontrollo aksesin (BusinessId)
            var currentBusinessId = _tenantProvider.GetBusinessId();
            if (!_tenantProvider.IsSuperAdmin() && reservation.BusinessId != currentBusinessId)
                throw new UnauthorizedAccessException("You are not allowed to delete this reservation.");

            // 3. Fshi pagesat
            if (reservation.Payments != null && reservation.Payments.Count > 0)
                _context.Payments.RemoveRange(reservation.Payments);

            // 4. Fshi historikun e statusit
            if (reservation.ReservationStatusHistories != null && reservation.ReservationStatusHistories.Count > 0)
                _context.ReservationStatusHistories.RemoveRange(reservation.ReservationStatusHistories);

            // 5. Fshi kontratën
            if (reservation.Contract != null)
                _context.Contracts.Remove(reservation.Contract);

            // 6. Fshi rezervimin
            _context.Reservations.Remove(reservation);

            // 7. Ruaj ndryshimet
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
