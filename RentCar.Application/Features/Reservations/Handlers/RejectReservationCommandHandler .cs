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
    //public class RejectReservationCommandHandler : IRequestHandler<RejectReservationCommand, bool>
    //{
    //    private readonly RentCarDbContext _context;

    //    public RejectReservationCommandHandler(RentCarDbContext context)
    //    {
    //        _context = context;
    //    }

    //    //public async Task<bool> Handle(RejectReservationCommand request, CancellationToken cancellationToken)
    //    //{
    //    //    var reservation = await _context.Reservations
    //    //        .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

    //    //    if (reservation == null)
    //    //        return false;

    //    //    // Assume "Rejected" status ID = 4 (adjust as needed)
    //    //    reservation.ReservationStatusId = 4;

    //    //    // Optionally: store rejection reason in Reservation.Description (or add a new field)
    //    //    reservation.Contract ??= new Domain.Entities.Contract
    //    //    {
    //    //        FileUrl = $"rejection_reason:{request.Reason}",
    //    //        ReservationId = reservation.Id,
    //    //        CreatedAt = System.DateTime.UtcNow
    //    //    };

    //    //    await _context.SaveChangesAsync(cancellationToken);
    //    //    return true;
    //    //}
    //}
}
