using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class GetReservationByIdQueryHandler : IRequestHandler<GetReservationByIdQuery, ReservationDto>
    {
        private readonly RentCarDbContext _context;

        public GetReservationByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDto?> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Car).ThenInclude(c => c.CarModel).ThenInclude(m => m.CarBrand)
                .Include(r => r.Client)
                .Include(r => r.Business)
                .Include(r => r.ReservationStatus)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null) return null;

            return new ReservationDto
            {
                Id = reservation.Id,
                CarId = reservation.CarId,
                CarModel = reservation.Car.CarModel.Name,
                CarBrand = reservation.Car.CarModel.CarBrand.Name,
                LicensePlate = reservation.Car.LicensePlate,
                ClientId = reservation.ClientId,
                ClientName = $"{reservation.Client.FirstName} {reservation.Client.LastName}",
                BusinessId = reservation.BusinessId,
                BusinessName = reservation.Business.CompanyName,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                TotalPrice = reservation.TotalPrice,
                Status = reservation.ReservationStatus.Name
            };
        }
    }
}
