using MediatR;
using RentCar.Application.Features.VehicleInspection.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleInspection.Handlers
{
    public class DeleteInspectionCommandHandler : IRequestHandler<DeleteInspectionCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public DeleteInspectionCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteInspectionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.VehicleInspection.FindAsync(request.Id);
            if (entity == null)
                return false;

            _context.VehicleInspection.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }

}
