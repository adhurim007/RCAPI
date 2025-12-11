using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Reservations;
using RentCar.Application.Features.Reservations.Queries;
using RentCar.Application.MultiTenancy;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class GetReservationsByBusinessIdQueryHandler
   : IRequestHandler<GetReservationsByBusinessIdQuery, List<ReservationDto>>
    {
        private readonly RentCarDbContext _context;

        public GetReservationsByBusinessIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationDto>> Handle(
            GetReservationsByBusinessIdQuery request,
            CancellationToken cancellationToken)
        {
            var reservations = await _context.Reservations 
                .Include(r => r.Car)
                    .ThenInclude(c => c.CarModel)
                        .ThenInclude(m => m.CarBrand) 
                .Include(r => r.Customer)
                .Include(r => r.Business)
                .Include(r => r.ReservationStatus)
                .Include(r => r.PickupLocation)
                .Include(r => r.DropoffLocation) 
                .Where(r => r.BusinessId == request.BusinessId)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync(cancellationToken);

            return reservations.Select(r => new ReservationDto
            {
                Id = r.Id,
                ReservationNumber = r.ReservationNumber, 
                CarId = r.CarId,
                CarBrand = r.Car.CarModel.CarBrand.Name,
                CarModel = r.Car.CarModel.Name,
                LicensePlate = r.Car.LicensePlate, 
                CustomerId = r.CustomerId,
                CustomerName = r.Customer.FullName, 
                BusinessId = r.BusinessId,
                BusinessName = r.Business.CompanyName, 
                PickupDate = r.PickupDate,
                DropoffDate = r.DropoffDate,
                TotalDays = r.TotalDays, 
                TotalPrice = r.TotalPrice,
                DepositAmount = r.DepositAmount ?? 0, 
                ReservationStatusId = r.ReservationStatusId,
                Status = r.ReservationStatus.Name, 
                PickupLocationId = r.PickupLocationId,
                PickupLocation = r.PickupLocation.Name, 
                DropoffLocationId = r.DropoffLocationId,
                DropoffLocation = r.DropoffLocation.Name, 
                Notes = r.Notes ?? ""
            }).ToList();
        }
    }

}
