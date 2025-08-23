using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Payments.Commands
{
    namespace RentCar.Application.Features.Payments.Commands
    {
        public record AddPaymentCommand(
            int ReservationId,
            decimal Amount,
            string PaymentMethod,
            bool IsConfirmed
        ) : IRequest<int>;
    }
}
