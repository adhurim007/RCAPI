using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Roles.Commands
{
    public record UpdateRoleCommand(string Id, string Name) : IRequest<bool>;
}
