using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Commands
{
    public record CancelReservationCommand(int ReservationId, string Reason) : IRequest<bool>; 
}
