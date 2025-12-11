using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class UpdateReservationCommandHandler : IRequestHandler<UpdateReservationCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateReservationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Car)
                    .ThenInclude(c => c.PricingRules)
                .Include(r => r.ExtraServices)
                .ThenInclude(es => es.ExtraService)
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return false;

            if (request.PickupDate >= request.DropoffDate)
                throw new Exception("Dropoff date must be after pickup date.");

          
            reservation.PickupDate = request.PickupDate;
            reservation.DropoffDate = request.DropoffDate;
            reservation.PickupLocationId = request.PickupLocationId;
            reservation.DropoffLocationId = request.DropoffLocationId;
            reservation.Notes = request.Notes; 
            int totalDays = (reservation.DropoffDate - reservation.PickupDate).Days;
            reservation.TotalDays = totalDays;

            
            decimal carTotal = 0; 
            for (int i = 0; i < totalDays; i++)
            {
                var currentDate = reservation.PickupDate.AddDays(i);
                decimal dayPrice = reservation.Car.DailyPrice;

                var rule = reservation.Car.PricingRules
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
            decimal extrasTotal = reservation.ExtraServices.Sum(es => es.TotalPrice); 
            decimal discount = request.Discount ?? 0; 
            decimal totalWithoutDiscount = carTotal + extrasTotal; 
            decimal finalTotal = totalWithoutDiscount - discount;
            if (finalTotal < 0) finalTotal = 0;  
            reservation.Discount = discount;
            reservation.TotalPriceWithoutDiscount = totalWithoutDiscount;
            reservation.TotalPrice = finalTotal;

           
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }

}
