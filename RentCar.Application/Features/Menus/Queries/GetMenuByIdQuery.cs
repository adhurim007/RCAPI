using MediatR;
using RentCar.Application.DTOs.MenuDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Queries
{
    public record GetMenuByIdQuery(int Id) : IRequest<MenuDto>;
}
