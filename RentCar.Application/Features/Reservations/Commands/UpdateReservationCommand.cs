using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Commands
{
    public record UpdateReservationCommand(
        int Id,
        DateTime StartDate,
        DateTime EndDate,
        decimal TotalPrice,
        int ReservationStatusId
    ) : IRequest<bool>;
}
