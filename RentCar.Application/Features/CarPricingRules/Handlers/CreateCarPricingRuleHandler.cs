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
    public class CreateCarPricingRuleHandler : IRequestHandler<CreateCarPricingRuleCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCarPricingRuleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = new CarPricingRule
            { 
                CarId = request.CarId,
                FromDate = (DateTime)request.FromDate,
                ToDate = (DateTime)request.ToDate,
                Value = request.PricePerDay,
                RuleType = "Custom"  
            };

            await _unitOfWork.CarPricingRules.AddAsync(rule);
            await _unitOfWork.SaveChangesAsync();
            //return rule.Id;
            return 1;
        }
    }

}
