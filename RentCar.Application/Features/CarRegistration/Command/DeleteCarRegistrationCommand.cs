using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarRegistration.Command
{
    public record DeleteCarRegistrationCommand(int Id) : IRequest<bool>;
}
