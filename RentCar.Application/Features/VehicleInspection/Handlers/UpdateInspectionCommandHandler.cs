using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.VehicleInspection.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleInspection.Handlers
{
    public class UpdateInspectionCommandHandler : IRequestHandler<UpdateInspectionCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateInspectionCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateInspectionCommand request, CancellationToken cancellationToken)
        {
            var inspection = await _context.VehicleInspection
                .Include(i => i.Photos)
                .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken);

            if (inspection == null)
                return false;

            inspection.Mileage = request.Mileage;
            inspection.FuelLevel = request.FuelLevel;
            inspection.TireCondition = request.TireCondition;
            inspection.OverallCondition = request.OverallCondition;

            // Remove old photos
            _context.VehicleInspectionPhoto.RemoveRange(inspection.Photos);

            // Add new photos
            if (request.Photos != null)
            {
                foreach (var url in request.Photos)
                {
                    _context.VehicleInspectionPhoto.Add(new VehicleInspectionPhoto
                    {
                        InspectionId = inspection.Id,
                        ImageUrl = url
                    });
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
