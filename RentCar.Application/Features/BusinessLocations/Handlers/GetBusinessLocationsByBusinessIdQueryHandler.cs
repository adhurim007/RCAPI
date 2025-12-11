using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.BusinessLocationDTO;
using RentCar.Application.Features.BusinessLocations.Command;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Handlers
{
    public class GetBusinessLocationsByBusinessIdQueryHandler
    : IRequestHandler<GetBusinessLocationsByBusinessIdQuery, List<BusinessLocationDto>>
    {
        private readonly RentCarDbContext _db;

        public GetBusinessLocationsByBusinessIdQueryHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<List<BusinessLocationDto>> Handle(
            GetBusinessLocationsByBusinessIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _db.BusinessLocations
                .Where(x => x.BusinessId == request.BusinessId)
                .Select(x => new BusinessLocationDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    StateId = x.StateId,
                    CityId = x.CityId
                })
                .ToListAsync(cancellationToken);
        }
    }

}
