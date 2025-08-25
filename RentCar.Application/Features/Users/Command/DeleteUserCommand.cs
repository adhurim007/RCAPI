using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Users.Command
{
    public record DeleteUserCommand(string Id) : IRequest<bool>;
}
