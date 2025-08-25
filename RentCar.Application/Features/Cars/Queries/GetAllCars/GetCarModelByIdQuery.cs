using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Queries.GetAllCars
{
    public record GetCarModelByIdQuery(int Id) : IRequest<CarModel?>;
}
