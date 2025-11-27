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
    public class DeleteBusinessLocationHandler
     : IRequestHandler<DeleteBusinessLocationCommand, bool>
    {
        private readonly RentCarDbContext _db;

        public DeleteBusinessLocationHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(
            DeleteBusinessLocationCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _db.BusinessLocations
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null) return false;

            _db.BusinessLocations.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
