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
    public class GetVehicleDamagesQueryHandler
    : IRequestHandler<GetVehicleDamagesQuery, List<VehicleDamageDto>>
    {
        private readonly RentCarDbContext _context;

        public GetVehicleDamagesQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<VehicleDamageDto>> Handle(
            GetVehicleDamagesQuery request,
            CancellationToken ct)
        {
            var query = _context.VehicleDamage
                .Include(x => x.Photos)
                .AsQueryable();

            if (request.ReservationId.HasValue)
            {
                query = query.Where(x => x.ReservationId == request.ReservationId);
            }

            return await query
                .OrderByDescending(x => x.CreatedAt)
                .Select(x => new VehicleDamageDto
                {
                    Id = x.Id,
                    ReservationId = x.ReservationId,
                    DamageType = x.DamageType,
                    Description = x.Description,
                    EstimatedCost = x.EstimatedCost,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt,
                    Photos = x.Photos.Select(p => p.ImageUrl).ToList()
                })
                .ToListAsync(ct);
        }
    }

}
