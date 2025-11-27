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
    public class UpdateExtraServiceCommandHandler : IRequestHandler<UpdateExtraServiceCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateExtraServiceCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateExtraServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.ExtraServices.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
                return false;

            entity.Name = request.Name;
            entity.PricePerDay = request.PricePerDay;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
