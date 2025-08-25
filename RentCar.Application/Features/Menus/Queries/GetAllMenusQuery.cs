using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Queries
{
    public record GetAllMenusQuery() : IRequest<List<Menu>>;
}
