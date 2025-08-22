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
    public class DeleteMenuCommandHandler : IRequestHandler<DeleteMenuCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public DeleteMenuCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteMenuCommand request, CancellationToken cancellationToken)
        {
            var menu = await _context.Menus.FindAsync(new object[] { request.Id }, cancellationToken);
            if (menu == null) return false;

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
