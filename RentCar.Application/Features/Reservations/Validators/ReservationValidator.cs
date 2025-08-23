using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Validators
{
    public class ReservationValidator : IReservationValidator
    {
        private readonly RentCarDbContext _context;

        public ReservationValidator(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<(bool IsValid, string ErrorMessage)> ValidateAsync(Reservation reservation)
        {
             
            if (reservation.StartDate >= reservation.EndDate)
                return (false, "Start date must be earlier than end date.");

            if (reservation.StartDate < DateTime.UtcNow.Date)
                return (false, "Start date must be today or later.");

             
            var car = await _context.Cars.FindAsync(reservation.CarId);
            if (car == null || !car.IsAvailable)
                return (false, "Car is not available.");

             
            bool overlap = await _context.Reservations
                .AnyAsync(r => r.CarId == reservation.CarId &&
                               r.ReservationStatusId == 2 && // 2 = Approved (active)
                               reservation.StartDate < r.EndDate &&
                               reservation.EndDate > r.StartDate);

            if (overlap)
                return (false, "Car is already reserved for the selected period.");

            return (true, string.Empty);
        }
    }
}
