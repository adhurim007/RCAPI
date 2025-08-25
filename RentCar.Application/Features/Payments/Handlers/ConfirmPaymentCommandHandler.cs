using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Payments.Commands;
using RentCar.Application.Notifications;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Payments.Handlers
{
    public class ConfirmPaymentCommandHandler : IRequestHandler<ConfirmPaymentCommand, bool>
    {
        private readonly RentCarDbContext _context;
        private readonly INotificationService _notificationService;
        public ConfirmPaymentCommandHandler(RentCarDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<bool> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

            if (payment == null)
                throw new Exception("Payment not found.");

            payment.IsConfirmed = true;
            payment.PaidAt = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            var reservation = await _context.Reservations.FindAsync(payment.ReservationId);

            // Notify client
            await _notificationService.SendEmailAsync(
                "client@email.com", // lookup real client email
                "Payment Confirmed",
                $"Your payment for reservation #{reservation.Id} has been confirmed."
            );

            // Notify business
            await _notificationService.SendEmailAsync(
                "business@email.com",
                "Payment Received",
                $"Reservation #{reservation.Id} has been successfully paid by the client."
            );

            return true;
        }
    }
}
