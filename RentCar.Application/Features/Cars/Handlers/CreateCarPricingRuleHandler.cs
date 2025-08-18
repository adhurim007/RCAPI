using MediatR;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
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
                FromDate = request.StartDate,
                ToDate = request.EndDate,
                Value = request.Price,
                RuleType = "Custom"  
            };

            await _unitOfWork.CarPricingRules.AddAsync(rule);
            await _unitOfWork.SaveChangesAsync();
            //return rule.Id;
            return 1;
        }
    }

}
