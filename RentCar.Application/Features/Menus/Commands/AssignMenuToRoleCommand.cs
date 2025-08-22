using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Commands
{
    public record AssignMenuToRoleCommand(Guid RoleId, int MenuId) : IRequest<bool>;
}
