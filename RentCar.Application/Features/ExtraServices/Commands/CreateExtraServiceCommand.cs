using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.ExtraServices.Commands
{

    public class CreateExtraServiceCommand : IRequest<int>
    {
        public string Name { get; set; }
        public decimal PricePerDay { get; set; }
    }
}
