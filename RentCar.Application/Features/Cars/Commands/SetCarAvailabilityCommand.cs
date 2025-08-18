using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands 
{
    public class SetCarAvailabilityCommand : IRequest<bool>
    {
        public Guid CarId { get; set; }
        public bool IsAvailable { get; set; }
    }
}
