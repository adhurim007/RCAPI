using MediatR; 
using RentCar.Application.Features.Cars.Commands; 
using RentCar.Persistence; 

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
            var model = new RentCar.Domain.Entities.CarModel
            {
                Name = request.Name,
                CarBrandId = request.CarBrandId
            };

            _context.CarModels.Add(model);
            await _context.SaveChangesAsync(cancellationToken);

            return model.Id;
        }
    }
}
