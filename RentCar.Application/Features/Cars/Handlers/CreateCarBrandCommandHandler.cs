using MediatR;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class CreateCarBrandCommandHandler : IRequestHandler<CreateCarBrandCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateCarBrandCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCarBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new CarBrand { Name = request.Name };
            _context.CarBrands.Add(brand);
            await _context.SaveChangesAsync(cancellationToken);
            return brand.Id;
        }
    }
}
