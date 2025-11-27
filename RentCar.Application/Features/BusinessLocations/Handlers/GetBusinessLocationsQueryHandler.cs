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
    public class GetBusinessLocationsQueryHandler
    : IRequestHandler<GetBusinessLocationsQuery, List<BusinessLocationDto>>
    {
        private readonly RentCarDbContext _db;

        public GetBusinessLocationsQueryHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<List<BusinessLocationDto>> Handle(
            GetBusinessLocationsQuery request,
            CancellationToken cancellationToken)
        {
            return await _db.BusinessLocations
                .Where(x => x.BusinessId == request.BusinessId)
                .Select(x => new BusinessLocationDto
                {
                    Id = x.Id,
                    BusinessId = x.BusinessId,
                    Name = x.Name,
                    Address = x.Address,
                    StateId = x.StateId,
                    StateName = x.State.Name,
                    CityId = x.CityId,
                    CityName = x.City.Name
                })
                .ToListAsync(cancellationToken);
        }
    }
}
