using MediatR;
using Microsoft.AspNetCore.Http;
using RentCar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public class UploadCarImageCommand : IRequest<List<CarImageDto>>
    {
        public int CarId { get; set; }
        public List<IFormFile> Files { get; set; } = new();
    }

}
