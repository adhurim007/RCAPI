using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public class DeleteCarImageCommand : IRequest<bool>
    {
        public int ImageId { get; set; }

        public DeleteCarImageCommand(int imageId)
        {
            ImageId = imageId;
        }
    }

}
