using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Payments.Commands
{
    public record ConfirmPaymentCommand(int PaymentId) : IRequest<bool>;
}
