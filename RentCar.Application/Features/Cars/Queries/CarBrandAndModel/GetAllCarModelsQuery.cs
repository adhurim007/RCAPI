using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Queries.CarBrandAndModel
{
    public record GetAllCarModelsQuery() : IRequest<List<CarModel>>;
}
