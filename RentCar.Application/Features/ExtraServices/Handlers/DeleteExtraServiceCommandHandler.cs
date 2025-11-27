using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.ExtraServices.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.ExtraServices.Handlers
{
    public class DeleteExtraServiceCommandHandler : IRequestHandler<DeleteExtraServiceCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public DeleteExtraServiceCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteExtraServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraServices.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return false;

            _context.ExtraServices.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
