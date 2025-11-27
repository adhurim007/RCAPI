using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Payments.Commands.RentCar.Application.Features.Payments.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Payments.Handlers
{
    public class AddPaymentCommandHandler : IRequestHandler<AddPaymentCommand, int>
    {
        private readonly RentCarDbContext _context;

        public AddPaymentCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(AddPaymentCommand request, CancellationToken cancellationToken)
        {
            var reservation = await _context.Reservations
                .FirstOrDefaultAsync(r => r.Id == request.ReservationId, cancellationToken);

            if (reservation == null)
                throw new Exception("Reservation not found.");

            // Create Payment
            var payment = new Payment
            {
                ReservationId = reservation.Id,
                Amount = request.Amount,
                //PaidAt = DateTime.UtcNow,
                //PaymentMethod = request.PaymentMethod,
                //IsConfirmed = request.IsConfirmed
            };

            _context.Payments.Add(payment);
            await _context.SaveChangesAsync(cancellationToken);

            return payment.Id;
        }
    }
}
