using MediatR;
using RentCar.Application.Features.Menus.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Handlers
{
    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateMenuCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = new Menu
            {
                Title = request.Title,
                Subtitle = request.Subtitle,
                Type = request.Type,        // e.g. "basic", "group", "collapsable"
                Link = request.Link,        // route like "/cars/list"
                Icon = request.Icon,
                ParentId = request.ParentId,
                Claim = request.Claim,
                Active = request.Active,
                SortNumber = request.SortNumber,
                CreatedBy = request.CreatedBy,
                CreatedOn = DateTime.UtcNow
            };

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync(cancellationToken);

            return menu.Id;
        }
    }
}
