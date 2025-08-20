using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Reservations.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Handlers
{
    public class CancelReservationCommandHandler : IRequestHandler<CancelReservationCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public CancelReservationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Payment)
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId, cancellationToken);

            if (reservation == null)
                throw new KeyNotFoundException("Reservation not found.");

            if (reservation.ReservationStatusId == 3)  
                throw new InvalidOperationException("Reservation already canceled.");


            reservation.ReservationStatusId = 3;  
            reservation.Car.IsAvailable = true;

            
            _context.ReservationStatusHistories.Add(new ReservationStatusHistory
            {
                ReservationId = reservation.Id,
                ReservationStatusId = 3,
                ChangedAt = DateTime.Now,
                ChangedBy = "System", 
                Note = $"Canceled: {request.Reason}"
            });


            if (reservation.Payment != null)
            {
                 
                if ((reservation.StartDate - DateTime.Now).TotalHours >= 48)
                {
                    
                    Console.WriteLine($"Refund {reservation.Payment.Amount} to client");
                }
                else
                {
                 
                    Console.WriteLine($"No full refund. Policy applies.");
                }
            }

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
