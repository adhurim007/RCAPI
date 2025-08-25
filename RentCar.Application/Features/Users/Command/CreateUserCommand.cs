using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Users.Command
{
    public record CreateUserCommand(
        string Email,
        string Password,
        string FirstName,
        string LastName,
        string Phone
    ) : IRequest<string>;  
}
