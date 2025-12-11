using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.VehicleDamage.Command;
using RentCar.Domain.Entities;
using RentCar.Domain.Enums;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Handlers
{
    public class CreateVehicleDamageCommandHandler
    : IRequestHandler<CreateVehicleDamageCommand, int>
    {
        private readonly RentCarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateVehicleDamageCommandHandler(
            RentCarDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<int> Handle(
            CreateVehicleDamageCommand request,
            CancellationToken ct)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(x => x.Id == request.ReservationId, ct);

            if (reservation == null)
                throw new Exception("Reservation not found");

            var damage = new Domain.Entities.VehicleDamage
            {
                ReservationId = request.ReservationId,
                BusinessId = reservation.BusinessId,
                DamageType = request.DamageType,
                Description = request.Description,
                EstimatedCost = request.EstimatedCost,
                Status = DamageStatus.Pending
            };

            _context.VehicleDamage.Add(damage);
            await _context.SaveChangesAsync(ct);

            if (request.Photos != null && request.Photos.Any())
            {
                var root = _env.WebRootPath
                           ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

                var folder = Path.Combine(
                    root,
                    "uploads",
                    "damages",
                    damage.Id.ToString());

                Directory.CreateDirectory(folder);

                foreach (var file in request.Photos)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var fullPath = Path.Combine(folder, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await file.CopyToAsync(stream, ct);

                    _context.VehicleDamagePhoto.Add(new VehicleDamagePhoto
                    {
                        DamageId = damage.Id,
                        ImageUrl = $"/uploads/damages/{damage.Id}/{fileName}" 
                    });
                }

                await _context.SaveChangesAsync(ct);
            }

            return damage.Id;
        }
    }

}
