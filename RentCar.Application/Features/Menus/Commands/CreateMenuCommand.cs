using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Commands
{
    public record CreateMenuCommand(string Name, string Route, string Icon) : IRequest<int>;
}
