using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleInspection.Commands
{
    public record DeleteInspectionCommand(int Id) : IRequest<bool>;

}
