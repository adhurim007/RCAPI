using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public class CreateCarModelCommand : IRequest<int>
    {
        public string Name { get; set; }
        public int CarBrandId { get; set; }
    }
}
