using MediatR;
using Microsoft.AspNetCore.Http;
using RentCar.Application.Features.BusinessLocations.Command; 
using RentCar.Persistence;
 
namespace RentCar.Application.Features.BusinessLocations.Handlers
{
    public class CreateBusinessLocationHandler
   : IRequestHandler<CreateBusinessLocationCommand, int>
    {
        private readonly RentCarDbContext _db;

        public CreateBusinessLocationHandler(RentCarDbContext db)
        {
            _db = db;
        }

        public async Task<int> Handle(
            CreateBusinessLocationCommand request,
            CancellationToken cancellationToken)
        {
            // ✅ MOS MERRE NGA CLAIMS – merre nga REQUEST
            if (request.BusinessId <= 0)
                throw new Exception("BusinessId is required");

            var entity = new Domain.Entities.BusinessLocations
            {
                BusinessId = request.BusinessId,
                Name = request.Name,
                Address = request.Address,
                StateId = request.StateId,
                CityId = request.CityId
            };

            _db.BusinessLocations.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }


}
