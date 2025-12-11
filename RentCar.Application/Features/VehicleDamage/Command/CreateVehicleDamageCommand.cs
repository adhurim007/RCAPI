using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Command
{
    public record CreateVehicleDamageCommand(
    int ReservationId,
    string? DamageType,
    string? Description,
    decimal EstimatedCost,
    List<IFormFile>? Photos
) : IRequest<int>;
}
