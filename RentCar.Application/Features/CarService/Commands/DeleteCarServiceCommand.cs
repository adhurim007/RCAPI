using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarService.Commands
{
    public record DeleteCarServiceCommand(int Id) : IRequest<bool>;
}
