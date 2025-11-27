using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.CarPricingRules.Queries;
using RentCar.Persistence;

namespace RentCar.Application.Features.CarPricingRules.Handlers
{
    public class GetAllCarPricingRulesHandler
        : IRequestHandler<GetAllCarPricingRulesQuery, List<CarPricingRuleDto>>
    {
        private readonly RentCarDbContext _context;

        public GetAllCarPricingRulesHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarPricingRuleDto>> Handle(GetAllCarPricingRulesQuery request, CancellationToken cancellationToken)
        {
            var rules = await _context.CarPricingRules
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return rules.Select(r => new CarPricingRuleDto
            {
                Id = r.Id,
                CarId = r.CarId,
                RuleType = r.RuleType,
                FromDate = r.FromDate,
                ToDate = r.ToDate,
                PricePerDay = r.PricePerDay,
                Description = r.Description,
                DaysOfWeek = r.DaysOfWeek
            }).ToList();
        }
    }
}
