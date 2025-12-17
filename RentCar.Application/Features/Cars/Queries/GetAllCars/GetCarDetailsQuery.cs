using MediatR;
using RentCar.Application.DTOs.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Queries.GetAllCars
{
    public record GetCarDetailsQuery(int Id) : IRequest<CarDetailsDto>;
}
