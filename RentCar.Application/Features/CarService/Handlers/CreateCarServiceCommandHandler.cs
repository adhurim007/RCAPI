using MediatR;
using RentCar.Application.Features.CarService.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarService.Handlers
{
    public class CreateCarServiceCommandHandler
        : IRequestHandler<CreateCarServiceCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateCarServiceCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCarServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.CarService
            {
                CarId = request.CarId,
                BusinessId = request.BusinessId,
                ServiceType = request.ServiceType,
                ServiceDate = request.ServiceDate,
                Mileage = request.Mileage,
                Cost = request.Cost,
                ServiceCenter = request.ServiceCenter,
                NextServiceDate = request.NextServiceDate,
                NextServiceMileage = request.NextServiceMileage,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.CarServices.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
