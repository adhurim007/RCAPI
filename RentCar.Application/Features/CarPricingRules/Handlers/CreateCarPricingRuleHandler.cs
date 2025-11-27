using MediatR;
using RentCar.Application.Features.CarPricingRules.Command;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Handlers
{
    public class CreateCarPricingRuleHandler
        : IRequestHandler<CreateCarPricingRuleCommand, int>
    {
        private readonly ICarPricingRuleRepository _carPricingRepository;

        public CreateCarPricingRuleHandler(ICarPricingRuleRepository carPricingRepository)
        {
            _carPricingRepository = carPricingRepository;
        }

        public async Task<int> Handle(CreateCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = new CarPricingRule
            {
                CarId = request.CarId,
                RuleType = string.IsNullOrWhiteSpace(request.RuleType)
                    ? "Custom"
                    : request.RuleType,
                PricePerDay = request.PricePerDay,
                FromDate = request.FromDate,
                ToDate = request.ToDate,
                DaysOfWeek = request.DaysOfWeek != null
                    ? string.Join(",", request.DaysOfWeek)
                    : null,
                Description = request.Description
            };

            await _carPricingRepository.AddAsync(rule);
             
            return rule.Id;
        }
    }

}
