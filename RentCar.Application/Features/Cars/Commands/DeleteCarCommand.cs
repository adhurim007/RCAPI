using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public class DeleteCarCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
