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
    public class DeleteCarBrandCommandHandler : IRequestHandler<DeleteCarBrandCommand>
    {
        private readonly RentCarDbContext _context;

        public DeleteCarBrandCommandHandler(RentCarDbContext context) => _context = context;

        public async Task<Unit> Handle(DeleteCarBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _context.CarBrands.FindAsync(new object[] { request.Id }, cancellationToken);
            if (brand == null) throw new KeyNotFoundException("Car brand not found");

            _context.CarBrands.Remove(brand);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<DeleteCarBrandCommand>.Handle(DeleteCarBrandCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
