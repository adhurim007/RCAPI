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

            menu.Title = request.Title;
            menu.Subtitle = request.Subtitle;
            menu.Type = request.Type;
            menu.Link = request.Link;
            menu.Icon = request.Icon;
            menu.ParentId = request.ParentId;
            menu.Claim = request.Claim;
            menu.Active = request.Active;
            menu.SortNumber = request.SortNumber;
            menu.LastModifiedBy = request.LastModifiedBy;
            menu.LastModifiedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
