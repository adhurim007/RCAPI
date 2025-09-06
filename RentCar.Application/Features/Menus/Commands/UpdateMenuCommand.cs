using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Commands
{
    public record UpdateMenuCommand(
    int Id,
    string Title,
    string? Subtitle,
    string? Type,
    string? Link,
    string? Icon,
    int? ParentId,
    string? Claim,
    bool Active,
    int SortNumber,
    Guid? LastModifiedBy
) : IRequest<bool>;
}
