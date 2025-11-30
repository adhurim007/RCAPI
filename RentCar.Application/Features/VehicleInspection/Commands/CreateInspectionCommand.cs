using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleInspection.Commands
{
    public record CreateInspectionCommand(
         int ReservationId,
         int Type,
         int Mileage,
         decimal FuelLevel,
         string? TireCondition,
         string? OverallCondition,
         List<string>? Photos
     ) : IRequest<int>;

}
