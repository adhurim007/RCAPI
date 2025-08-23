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
    public class CreateCarModelCommandHandler : IRequestHandler<CreateCarModelCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateCarModelCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCarModelCommand request, CancellationToken cancellationToken)
        {
            var model = new CarModel { Name = request.Name };
            _context.CarModels.Add(model);
            await _context.SaveChangesAsync(cancellationToken);
            return model.Id;
        }
    }
}
