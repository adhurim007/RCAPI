using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Command
{
    public record DeleteCarPricingRuleCommand(int Id) : IRequest<bool>; 
}
