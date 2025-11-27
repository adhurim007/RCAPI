using MediatR;
using RentCar.Application.Features.CarPricingRules.Command;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Handlers
{
    public class CreateCarPricingRuleCommandHandler : IRequestHandler<CreateCarPricingRuleCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateCarPricingRuleCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = new CarPricingRule
            {
                CarId = request.CarId,
                RuleType = request.RuleType,
                PricePerDay = request.PricePerDay,
                FromDate = (DateTime)request.FromDate,
                ToDate = (DateTime)request.ToDate, 
                Description = request.Description
            };

            _context.CarPricingRules.Add(rule);
            await _context.SaveChangesAsync(cancellationToken);
            return rule.Id;
        }
    }

}
