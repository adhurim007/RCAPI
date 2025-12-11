using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.VehicleDamage.Command;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Handlers
{
    public class UpdateVehicleDamageCommandHandler
    : IRequestHandler<UpdateVehicleDamageCommand, bool>
    {
        private readonly RentCarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UpdateVehicleDamageCommandHandler(
            RentCarDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<bool> Handle(
            UpdateVehicleDamageCommand request,
            CancellationToken ct)
        {
            var damage = await _context.VehicleDamage
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (damage == null)
                return false;

            damage.DamageType = request.DamageType;
            damage.Description = request.Description;
            damage.EstimatedCost = request.EstimatedCost;
            damage.Status = request.Status;

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

            return true;
        }
    }

}
