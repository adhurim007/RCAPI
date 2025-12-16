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
    public class UpdateCarServiceCommandHandler
         : IRequestHandler<UpdateCarServiceCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateCarServiceCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCarServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.CarServices.FindAsync(request.Id);
            if (entity == null)
                return false;

            entity.ServiceType = request.ServiceType;
            entity.ServiceDate = request.ServiceDate;
            entity.Mileage = request.Mileage;
            entity.Cost = request.Cost;
            entity.ServiceCenter = request.ServiceCenter;
            entity.NextServiceDate = request.NextServiceDate;
            entity.NextServiceMileage = request.NextServiceMileage;
            entity.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
