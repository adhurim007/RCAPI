using MediatR;
using RentCar.Application.DTOs.VehicleInspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleInspection.Queries
{
    public record GetInspectionsByReservationQuery(int ReservationId)
        : IRequest<List<VehicleInspectionDto>>;
}
