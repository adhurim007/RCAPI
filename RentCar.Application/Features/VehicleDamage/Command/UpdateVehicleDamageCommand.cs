using MediatR;
using Microsoft.AspNetCore.Http;
using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Command
{
    public record UpdateVehicleDamageCommand(
    int Id,
    string? DamageType,
    string? Description,
    decimal EstimatedCost,
    DamageStatus Status,
    List<IFormFile>? Photos
) : IRequest<bool>;
}
