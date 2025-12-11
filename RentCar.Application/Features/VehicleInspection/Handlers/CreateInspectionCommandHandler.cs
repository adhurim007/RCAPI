using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.VehicleInspection.Commands;
using RentCar.Domain.Entities;
using RentCar.Domain.Enums;
using RentCar.Persistence;

namespace RentCar.Application.Features.VehicleInspection.Handlers
{
    public class CreateInspectionCommandHandler
    : IRequestHandler<CreateInspectionCommand, int>
    {
        private readonly RentCarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateInspectionCommandHandler(
            RentCarDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<int> Handle(CreateInspectionCommand request, CancellationToken ct)
        {
            var reservation = await _context.Reservations.Include(r => r.Car).FirstOrDefaultAsync(r => r.Id == request.ReservationId);

            if (reservation == null)
                throw new Exception("Reservation not found");

            var inspection = new Domain.Entities.VehicleInspection
            {
                ReservationId = request.ReservationId,
                BusinessId = reservation.BusinessId,
                Type = (InspectionType)request.Type,
                Mileage = request.Mileage,
                FuelLevel = request.FuelLevel,
                TireCondition = request.TireCondition,
                OverallCondition = request.OverallCondition
            };

            _context.VehicleInspection.Add(inspection);
            await _context.SaveChangesAsync(ct);

            var webRoot = _env.WebRootPath
                    ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var uploadsPath = Path.Combine(
                webRoot,
                "uploads",
                "inspections",
                inspection.Id.ToString()
            );

            Directory.CreateDirectory(uploadsPath);

            foreach (var file in request.Photos)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var fullPath = Path.Combine(uploadsPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream, ct);

                _context.VehicleInspectionPhoto.Add(new VehicleInspectionPhoto
                {
                    InspectionId = inspection.Id,
                    ImageUrl = $"/uploads/inspections/{inspection.Id}/{fileName}"
                });
            }

            await _context.SaveChangesAsync(ct);
            return inspection.Id;
        }
    }


}
