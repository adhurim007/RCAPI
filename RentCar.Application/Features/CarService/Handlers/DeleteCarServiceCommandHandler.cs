using MediatR;
using RentCar.Application.Features.CarService.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarService.Handlers
{
    public class DeleteCarServiceCommandHandler
        : IRequestHandler<DeleteCarServiceCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public DeleteCarServiceCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCarServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.CarServices.FindAsync(request.Id);
            if (entity == null)
                return false;

            _context.CarServices.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
