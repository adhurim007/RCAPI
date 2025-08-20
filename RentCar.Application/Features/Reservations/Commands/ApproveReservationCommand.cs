using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Commands
{
    public record ApproveReservationCommand(int Id, int ApprovedBy) : IRequest<bool>;
}
