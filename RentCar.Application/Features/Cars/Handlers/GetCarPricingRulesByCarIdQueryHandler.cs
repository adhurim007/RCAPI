using MediatR;
using RentCar.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.CarPricingRules.Queries;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarPricingRulesByCarIdQueryHandler : IRequestHandler<GetCarPricingRulesByCarIdQuery, List<CarPricingRuleDto>>
    {
        private readonly ICarPricingRuleRepository _repository;

        public GetCarPricingRulesByCarIdQueryHandler(ICarPricingRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CarPricingRuleDto>> Handle(GetCarPricingRulesByCarIdQuery request, CancellationToken cancellationToken)
        {
            var rules = await _repository.GetByCarIdAsync(request.CarId); // This must return a List<CarPricingRule>

            return rules.Select(rule => new CarPricingRuleDto
            {
                Id = rule.Id,
                CarId = rule.CarId,
                FromDate = (DateTime)rule.FromDate,
                ToDate = (DateTime)rule.ToDate,
                PricePerDay = rule.PricePerDay
            }).ToList();
        }
         
    }
}
