using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Roles.Queries
{
    public record GetAllRolesQuery() : IRequest<List<IdentityRole<Guid>>>;
}
