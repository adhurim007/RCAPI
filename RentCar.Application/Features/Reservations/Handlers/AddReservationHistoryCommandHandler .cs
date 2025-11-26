using MediatR;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    //public class AddReservationHistoryCommandHandler
    //    : IRequestHandler<AddReservationHistoryCommand, int>
    //{
    //    private readonly RentCarDbContext _context;

    //    public AddReservationHistoryCommandHandler(RentCarDbContext context)
    //    {
    //        _context = context;
    //    }

    //    //public async Task<int> Handle(AddReservationHistoryCommand request, CancellationToken cancellationToken)
    //    //{
    //    //    var history = new ReservationStatusHistory
    //    //    {
    //    //        ReservationId = request.ReservationId,
    //    //        ReservationStatusId = request.ReservationStatusId,
    //    //        ChangedAt = DateTime.UtcNow,
    //    //        ChangedBy = request.ChangedBy,
    //    //        Note = request.Note
    //    //    };

    //    //    _context.ReservationStatusHistories.Add(history);
    //    //    await _context.SaveChangesAsync(cancellationToken);

    //    //    return history.Id;
    //    //}
    //}
}
