using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Payments.Commands;
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

        public ConfirmPaymentCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(ConfirmPaymentCommand request, CancellationToken cancellationToken)
        {
            var payment = await _context.Payments
                .FirstOrDefaultAsync(p => p.Id == request.PaymentId, cancellationToken);

            if (payment == null)
                throw new Exception("Payment not found.");

            payment.IsConfirmed = true;
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
