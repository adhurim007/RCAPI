using MediatR;
using RentCar.Application.DTOs.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Queries
{
    public record GetCarPricingRulesByCarIdQuery(int CarId) : IRequest<List<CarPricingRuleDto>>;
}
