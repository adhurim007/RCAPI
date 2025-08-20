using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Application.Features.Reservations.Validators;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, int>
    {
        private readonly RentCarDbContext _context;
        private readonly IReservationValidator _validator;

        public CreateReservationCommandHandler(RentCarDbContext context, IReservationValidator validator)
        {
            _context = context;
            _validator = validator;
        }

        public async Task<int> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = new Reservation
            {
                CarId = request.CarId,
                ClientId = request.ClientId,
                BusinessId = request.BusinessId,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ReservationStatusId = 1 
            };

         
            var (isValid, errorMessage) = await _validator.ValidateAsync(reservation);
            if (!isValid)
                throw new InvalidOperationException(errorMessage);

          
            var car = await _context.Cars
                .Include(c => c.PricingRules)
                .FirstOrDefaultAsync(c => c.Id == reservation.CarId);

            if (car == null)
                throw new InvalidOperationException("Car not found.");

            int days = (reservation.EndDate - reservation.StartDate).Days;
            decimal totalPrice = 0;

            for (int i = 0; i < days; i++)
            {
                var currentDate = reservation.StartDate.AddDays(i);
                decimal dayPrice = car.DailyPrice;

              
                var rule = car.PricingRules
                    .FirstOrDefault(r => currentDate >= r.FromDate && currentDate <= r.ToDate);

                if (rule != null)
                {
                    if (rule.RuleType == "Discount")
                        dayPrice -= rule.Value;
                    else if (rule.RuleType == "Increase")
                        dayPrice += rule.Value;
                }

                totalPrice += dayPrice;
            }

            reservation.TotalPrice = totalPrice;
             
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(cancellationToken);

            return reservation.Id;
        }
    }

}
