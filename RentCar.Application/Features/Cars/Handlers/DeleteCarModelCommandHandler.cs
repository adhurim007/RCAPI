using MediatR;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class DeleteCarModelCommandHandler : IRequestHandler<DeleteCarModelCommand>
    {
        private readonly RentCarDbContext _context;

        public DeleteCarModelCommandHandler(RentCarDbContext context) => _context = context;

        public async Task<Unit> Handle(DeleteCarModelCommand request, CancellationToken cancellationToken)
        {
            var model = await _context.CarModels.FindAsync(new object[] { request.Id }, cancellationToken);
            if (model == null) throw new KeyNotFoundException("Car model not found");

            _context.CarModels.Remove(model);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<DeleteCarModelCommand>.Handle(DeleteCarModelCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
