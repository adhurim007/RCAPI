using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public class UpdateCarImageCommand : IRequest
    {
        public Guid CarId { get; set; }
        public string ImageUrl { get; set; }
    } 
}
