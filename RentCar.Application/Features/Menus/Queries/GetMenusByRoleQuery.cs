using MediatR;
using RentCar.Domain.Entities;

namespace RentCar.Application.Features.Menus.Queries
{
    public record GetMenusByRoleQuery(Guid RoleId) : IRequest<List<Menu>>;
}
