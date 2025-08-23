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
    public class UpdateCarBrandCommandHandler : IRequestHandler<UpdateCarBrandCommand>
    {
        private readonly RentCarDbContext _context;

        public UpdateCarBrandCommandHandler(RentCarDbContext context) => _context = context;

        public async Task<Unit> Handle(UpdateCarBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _context.CarBrands.FindAsync(new object[] { request.Id }, cancellationToken);
            if (brand == null) throw new KeyNotFoundException("Car brand not found");

            brand.Name = request.Name;
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        Task IRequestHandler<UpdateCarBrandCommand>.Handle(UpdateCarBrandCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
