using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Command
{
    public record CreateBusinessLocationCommand(
    int BusinessId,
    string Name,
    string? Address,
    int StateId,
    int CityId
    ) : IRequest<int>;
}
