using MediatR;
using RentCar.Application.DTOs.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarService.Queries
{
    public record GetCarServicesByBusinessIdQuery(int BusinessId) : IRequest<List<CarServiceDto>>;
}
