using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.VehicleDamage.Command;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Handlers
{
    public class DeleteVehicleDamageCommandHandler
    : IRequestHandler<DeleteVehicleDamageCommand, bool>
    {
        private readonly RentCarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteVehicleDamageCommandHandler(
            RentCarDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<bool> Handle(
            DeleteVehicleDamageCommand request,
            CancellationToken ct)
        {
            var damage = await _context.VehicleDamage
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == request.Id, ct);

            if (damage == null)
                return false;

            // delete files from disk
            var root = _env.WebRootPath
                       ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");

            var folder = Path.Combine(
                root,
                "uploads",
                "damages",
                damage.Id.ToString());

            if (Directory.Exists(folder))
                Directory.Delete(folder, true);

            _context.VehicleDamagePhoto.RemoveRange(damage.Photos);
            _context.VehicleDamage.Remove(damage);

            await _context.SaveChangesAsync(ct);

            return true;
        }
    }

}
