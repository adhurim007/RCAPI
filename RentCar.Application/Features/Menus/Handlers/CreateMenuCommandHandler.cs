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
                Name = request.Name,
                Route = request.Route,
                Icon = request.Icon
            };

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync(cancellationToken);

            return menu.Id;
        }
    }
}
