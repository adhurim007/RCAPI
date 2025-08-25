using MediatR;
using RentCar.Application.DTOs.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Queries
{
    public record GetAllReservationsQuery() : IRequest<List<ReservationDto>>;
}
