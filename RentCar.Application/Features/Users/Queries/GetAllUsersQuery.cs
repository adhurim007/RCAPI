using MediatR;
using RentCar.Application.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Users.Queries
{
    public record GetAllUsersQuery() : IRequest<List<UserDto>>;
}
