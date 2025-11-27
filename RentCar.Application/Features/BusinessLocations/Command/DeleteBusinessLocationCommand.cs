using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Command
{

    public record DeleteBusinessLocationCommand(int Id) : IRequest<bool>;
}
