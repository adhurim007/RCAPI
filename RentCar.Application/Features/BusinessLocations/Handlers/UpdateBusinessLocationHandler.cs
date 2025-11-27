using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.BusinessLocations.Command;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Handlers
{
    public class UpdateBusinessLocationHandler
    : IRequestHandler<UpdateBusinessLocationCommand, bool>
    {
        private readonly RentCarDbContext _db;

        public UpdateBusinessLocationHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(
            UpdateBusinessLocationCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _db.BusinessLocations.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null) return false;

            entity.Name = request.Name;
            entity.Address = request.Address;
            entity.StateId = request.StateId;
            entity.CityId = request.CityId;

            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
