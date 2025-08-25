using MediatR;
using RentCar.Application.Features.Menus.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Menus.Handlers
{
    public class UpdateMenuCommandHandler : IRequestHandler<UpdateMenuCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateMenuCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(new object[] { request.Id }, cancellationToken);
            if (menu == null) return false;

            menu.Name = request.Name;
            menu.Route = request.Route;
            menu.Icon = request.Icon;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
