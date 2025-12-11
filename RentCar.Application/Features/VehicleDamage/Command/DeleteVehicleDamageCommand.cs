using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Command
{
    public record DeleteVehicleDamageCommand(int Id) : IRequest<bool>;
}
