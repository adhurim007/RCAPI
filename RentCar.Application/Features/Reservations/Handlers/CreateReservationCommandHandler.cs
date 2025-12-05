using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Application.Features.Reservations.Validators;
using RentCar.Application.Notifications;
using RentCar.Application.Pricing;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class CreateReservationCommandHandler
    : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateReservationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var year = DateTime.UtcNow.Year;

            // Merr totalin e rezervimeve ekzistuese për këtë vit
            var totalReservationsThisYear = await _context.Reservations
                .Where(r => r.CreatedAt.Year == year)   
                .CountAsync(cancellationToken);

            var sequence = totalReservationsThisYear + 1;

            var reservationNumber = $"{sequence}/{year}";


            if (request.PickupDate >= request.DropoffDate)
                throw new Exception("Dropoff date must be after pickup date.");
             
            var car = await _context.Cars
                .Include(c => c.PricingRules)
                .FirstOrDefaultAsync(c => c.Id == request.CarId);

            if (car == null)
                throw new Exception("Car not found");
             
            var extraServiceIds = request.ExtraServices.Select(x => x.ExtraServiceId).ToList();

            var extraServiceMaster = await _context.ExtraServices
                .Where(e => extraServiceIds.Contains(e.Id))
                .ToListAsync();
             
            int totalDays = (request.DropoffDate - request.PickupDate).Days;
             
            decimal carTotal = 0;

            for (int i = 0; i < totalDays; i++)
            {
                var currentDate = request.PickupDate.AddDays(i);
                decimal dayPrice = car.DailyPrice;

                var rule = car.PricingRules
                    .FirstOrDefault(r => currentDate >= r.FromDate && currentDate <= r.ToDate);

                if (rule != null)
                {
                    if (rule.RuleType == "Discount")
                        dayPrice -= rule.PricePerDay;
                    else if (rule.RuleType == "Increase")
                        dayPrice += rule.PricePerDay;
                }

                carTotal += dayPrice;
            }
             
            decimal extrasTotal = 0;
            var reservationExtraEntities = new List<ReservationExtraService>();

            foreach (var item in request.ExtraServices)
            {
                var service = extraServiceMaster.FirstOrDefault(e => e.Id == item.ExtraServiceId);
                if (service == null) continue;

                var totalServicePrice = service.PricePerDay * item.Quantity * totalDays;
                extrasTotal += totalServicePrice;

                reservationExtraEntities.Add(new ReservationExtraService
                {
                    ExtraServiceId = service.Id,
                    Quantity = item.Quantity,
                    PricePerDay = service.PricePerDay,
                    TotalPrice = totalServicePrice
                });
            }
             
            decimal totalWithoutDiscount = carTotal + extrasTotal;
             
            decimal discount = request.Discount ?? 0;
            decimal finalTotal = totalWithoutDiscount - discount;
            if (finalTotal < 0) finalTotal = 0;

           
            var reservation = new Reservation
            {
                ReservationNumber = reservationNumber,
                CarId = request.CarId,
                CustomerId = request.CustomerId,
                PickupLocationId = request.PickupLocationId,
                DropoffLocationId = request.DropoffLocationId,
                PickupDate = request.PickupDate,
                DropoffDate = request.DropoffDate,
                TotalDays = totalDays,
                BasePricePerDay = car.DailyPrice,
                TotalPriceWithoutDiscount = totalWithoutDiscount,
                Discount = request.Discount,
                TotalPrice = finalTotal, 
                ReservationStatusId = 1,
                BusinessId = car.BusinessId,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            reservation.ExtraServices = reservationExtraEntities;
             
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }
    }

}
