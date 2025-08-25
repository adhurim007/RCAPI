using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Commands
{
    public record AddReservationHistoryCommand(
        int ReservationId,
        int ReservationStatusId,
        string ChangedBy,
        string Note
    ) : IRequest<int>;
}
