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
    public class GetReservationByIdQueryHandler
       : IRequestHandler<GetReservationByIdQuery, ReservationDto?>
    {
        private readonly RentCarDbContext _context;

        public GetReservationByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<ReservationDto?> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations

                .Include(r => r.Car)
                    .ThenInclude(c => c.CarModel)
                        .ThenInclude(m => m.CarBrand)

                .Include(r => r.Customer)

                .Include(r => r.Business)

                .Include(r => r.ReservationStatus)

                .Include(r => r.PickupLocation)
                .Include(r => r.DropoffLocation)

                .Include(r => r.ExtraServices)
                    .ThenInclude(es => es.ExtraService)

                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return null;

            return new ReservationDto
            {
                Id = reservation.Id,
                ReservationNumber = reservation.ReservationNumber,

                CarId = reservation.CarId,
                CarBrand = reservation.Car.CarModel.CarBrand.Name,
                CarModel = reservation.Car.CarModel.Name,
                LicensePlate = reservation.Car.LicensePlate,

                CustomerId = reservation.CustomerId,
                PersonalNumber = reservation.Customer.DocumentNumber,
                FirstName = reservation.Customer.FullName.Split(" ").FirstOrDefault(),
                LastName = reservation.Customer.FullName.Split(" ").Skip(1).FirstOrDefault(),
                PhoneNumber = reservation.Customer.PhoneNumber,
                Address = reservation.Customer.Address,

                BusinessId = reservation.BusinessId,
                BusinessName = reservation.Business.CompanyName,

                PickupDate = reservation.PickupDate,
                DropoffDate = reservation.DropoffDate,
                TotalDays = reservation.TotalDays,

                PickupLocationId = reservation.PickupLocationId,
                DropoffLocationId = reservation.DropoffLocationId,
                PickupLocation = reservation.PickupLocation?.Name,
                DropoffLocation = reservation.DropoffLocation?.Name,

                TotalPrice = reservation.TotalPrice,
                TotalWithoutDiscount = reservation.TotalPriceWithoutDiscount,
                Discount = reservation.Discount ?? 0,

                ExtraServices = reservation.ExtraServices
                    .Select(x => new ReservationExtraServiceDto
                    {
                        ExtraServiceId = x.ExtraServiceId,
                        Quantity = x.Quantity,
                        PricePerDay = x.PricePerDay,
                        TotalPrice = x.TotalPrice
                    }).ToList()
            };
        }

    }
}
