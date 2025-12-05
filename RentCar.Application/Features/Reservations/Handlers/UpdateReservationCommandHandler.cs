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
                .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

            if (reservation == null)
                return false;

            if (request.PickupDate >= request.DropoffDate)
                throw new Exception("Dropoff date must be after pickup date.");

            // Update basic fields
            reservation.PickupDate = request.PickupDate;
            reservation.DropoffDate = request.DropoffDate;
            reservation.PickupLocationId = request.PickupLocationId;
            reservation.DropoffLocationId = request.DropoffLocationId;
            reservation.Notes = request.Notes;
            reservation.ReservationStatusId = request.ReservationStatusId;

            // Recalculate days
            int totalDays = (reservation.DropoffDate - reservation.PickupDate).Days;
            reservation.TotalDays = totalDays;

            // Recalculate price using pricing rules
            decimal totalPrice = 0;

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

                totalPrice += dayPrice;
            }

            reservation.TotalPrice = totalPrice;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
