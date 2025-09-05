using MediatR;
using RentCar.Application.DTOs.MenuDto;
using RentCar.Domain.Entities;

namespace RentCar.Application.Features.Menus.Queries
{
    public class GetMenusForUserQuery : IRequest<List<MenuDto>> { }
}
