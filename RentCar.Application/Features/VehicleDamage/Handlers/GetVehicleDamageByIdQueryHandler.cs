using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.VehicleDamage;
using RentCar.Application.Features.VehicleDamage.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleDamage.Handlers
{
    public class GetVehicleDamageByIdQueryHandler
    : IRequestHandler<GetVehicleDamageByIdQuery, VehicleDamageDto>
    {
        private readonly RentCarDbContext _context;

        public GetVehicleDamageByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<VehicleDamageDto> Handle(GetVehicleDamageByIdQuery request, CancellationToken cancellationToken)
        {
            var damage = await _context.VehicleDamage
                .Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (damage == null)
                return null;

            return new VehicleDamageDto
            {
                Id = damage.Id,
                ReservationId = damage.ReservationId,
                DamageType = damage.DamageType,
                Description = damage.Description,
                EstimatedCost = damage.EstimatedCost,
                Status = damage.Status,
                Photos = damage.Photos.Select(p => p.ImageUrl).ToList()
            };
        }
    }
}
