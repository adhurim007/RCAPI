using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Roles.Commands
{
    public record CreateRoleCommand(string RoleName) : IRequest<bool>;
}
