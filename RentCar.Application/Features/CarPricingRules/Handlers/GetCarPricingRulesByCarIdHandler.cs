using MediatR;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.CarPricingRules.Queries;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Handlers
{
    public class GetCarPricingRulesByCarIdHandler
        : IRequestHandler<GetCarPricingRulesByCarIdQuery, List<CarPricingRuleDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCarPricingRulesByCarIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CarPricingRuleDto>> Handle(GetCarPricingRulesByCarIdQuery request, CancellationToken cancellationToken)
        {
            var rules = await _unitOfWork.CarPricingRules
                .GetAllAsync();

            return rules.Select(rule => new CarPricingRuleDto
            {
                Id = rule.Id,
                CarId = rule.CarId,
                RuleType = rule.RuleType,
                PricePerDay = rule.PricePerDay,
                FromDate = rule.FromDate,
                ToDate = rule.ToDate,
                DaysOfWeek = rule.DaysOfWeek,
                Description = rule.Description
            }).ToList();
        }
    }
}
