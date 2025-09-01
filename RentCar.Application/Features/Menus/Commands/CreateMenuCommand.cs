using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Commands
{
    public record CreateMenuCommand(
    string Title,
    string? Subtitle,
    string? Type,          // "basic", "group", "collapsable"
    string? Link,          // e.g. "/cars/list"
    string? Icon,
    int? ParentId,
    string? Claim,
    bool Active,
    int SortNumber,
    Guid? CreatedBy
) : IRequest<int>;

}
