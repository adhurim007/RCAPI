﻿using MediatR;
using Microsoft.AspNetCore.Http;
using RentCar.Application.DTOs.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public record UpdateCarImageCommand(int ImageId, IFormFile NewImage) : IRequest<CarImageDto>;
}
