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
    public class GetAllBusinessLocationsQueryHandler
    : IRequestHandler<GetAllBusinessLocationsQuery, List<BusinessLocationDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllBusinessLocationsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<BusinessLocationDto>> Handle(GetAllBusinessLocationsQuery request, CancellationToken cancellationToken)
        {
            return await _context.BusinessLocations
                .Include(x => x.State)
                .Include(x => x.City)
                .Select(x => new BusinessLocationDto
                {
                    Id = x.Id,
                    BusinessId = x.BusinessId,
                    Name = x.Name,
                    Address = x.Address,
                    StateId = x.StateId,
                    CityId = x.CityId,
                    StateName = x.State.Name,
                    CityName = x.City.Name
                })
                .OrderBy(x => x.BusinessId)
                .ThenBy(x => x.Name)
                .ToListAsync(cancellationToken);
        }
    }
}
