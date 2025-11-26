using MediatR;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.CarPricingRules.Queries;
using RentCar.Domain.Interfaces;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarPricingRuleByIdQueryHandler : IRequestHandler<GetCarPricingRuleByIdQuery, CarPricingRuleDto?>
    {
        private readonly RentCarDbContext _context;

        public GetCarPricingRuleByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<CarPricingRuleDto?> Handle(GetCarPricingRuleByIdQuery request, CancellationToken cancellationToken)
        {
            var rule = await _context.CarPricingRules.FindAsync(request.Id);
            if (rule == null) return null;

            return new CarPricingRuleDto
            {
                Id = rule.Id,
                CarId = rule.CarId,
                RuleType = rule.RuleType,
                Description = rule.Description,
                PricePerDay = rule.PricePerDay,
                DaysOfWeek = rule.DaysOfWeek,
                FromDate = rule.FromDate,
                ToDate = rule.ToDate
            };
        } 
    }
}
