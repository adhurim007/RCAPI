using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Persistence;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class GetAllReservationsQueryHandler
        : IRequestHandler<GetAllReservationsQuery, List<ReservationDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllReservationsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationDto>> Handle(
            GetAllReservationsQuery request,
            CancellationToken cancellationToken)
        {
            var reservations = await _context.Reservations
                .Include(r => r.Car)
                    .ThenInclude(c => c.CarModel)
                        .ThenInclude(m => m.CarBrand)
                .Include(r => r.Customer)    
                .ToListAsync(cancellationToken);

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id, 
                CarId = r.CarId,
                ReservationNumber = r.ReservationNumber,
                CarName = $"{r.Car.CarModel.CarBrand.Name} {r.Car.CarModel.Name}", 
                CustomerId = r.CustomerId,
                CustomerName = r.Customer.FullName,    
                BusinessId = r.BusinessId,
                PickupDate = r.PickupDate,
                DropoffDate = r.DropoffDate,
                TotalPrice = r.TotalPrice, 
                ReservationStatusId = r.ReservationStatusId, 
            })
            .ToList();
        }
    }
}
