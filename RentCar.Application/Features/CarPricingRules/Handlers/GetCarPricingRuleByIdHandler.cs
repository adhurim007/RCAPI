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
    public class GetCarPricingRuleByIdHandler
            : IRequestHandler<GetCarPricingRuleByIdQuery, CarPricingRuleDto?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCarPricingRuleByIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CarPricingRuleDto?> Handle(GetCarPricingRuleByIdQuery request, CancellationToken cancellationToken)
        {
            var rule = await _unitOfWork.CarPricingRules.GetAsync(request.Id);
            if (rule == null) return null;

            return new CarPricingRuleDto
            {
                Id = rule.Id,
                CarId = rule.CarId,
                RuleType = rule.RuleType,
                PricePerDay = rule.PricePerDay,
                FromDate = rule.FromDate,
                ToDate = rule.ToDate,
                DaysOfWeek = rule.DaysOfWeek,
                Description = rule.Description
            };
        }
    }
}
