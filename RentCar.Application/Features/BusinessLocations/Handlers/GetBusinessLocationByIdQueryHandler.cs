using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.BusinessLocationDTO;
using RentCar.Application.Features.BusinessLocations.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Handlers
{
    public class GetBusinessLocationByIdQueryHandler
        : IRequestHandler<GetBusinessLocationByIdQuery, BusinessLocationDto>
    {
        private readonly RentCarDbContext _db;

        public GetBusinessLocationByIdQueryHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<BusinessLocationDto> Handle(
            GetBusinessLocationByIdQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _db.BusinessLocations
                .Include(x => x.State)
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null) return null;

            return new BusinessLocationDto
            {
                Id = entity.Id,
                BusinessId = entity.BusinessId,
                Name = entity.Name,
                Address = entity.Address,
                StateId = entity.StateId,
                StateName = entity.State.Name,
                CityId = entity.CityId,
                CityName = entity.City.Name
            };
        }
    }
}
