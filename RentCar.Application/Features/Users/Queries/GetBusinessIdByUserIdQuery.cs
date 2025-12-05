using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Users.Queries
{
    public record GetBusinessIdByUserIdQuery(string UserId) : IRequest<Business?>;
}
