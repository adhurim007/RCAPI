using MediatR;
using RentCar.Application.DTOs.VehicleDamage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Queries
{
    public record GetVehicleDamagesQuery(
    int? ReservationId
) : IRequest<List<VehicleDamageDto>>;
}
