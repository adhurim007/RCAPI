using MediatR;
using RentCar.Application.Features.CarPricingRules.Command;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Handlers
{
    public class UpdateCarPricingRuleCommandHandler : IRequestHandler<UpdateCarPricingRuleCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateCarPricingRuleCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _context.CarPricingRules.FindAsync(request.Id);
            if (rule == null) return false;

            rule.RuleType = request.RuleType;
            rule.PricePerDay = request.PricePerDay;
            rule.FromDate = (DateTime)request.FromDate;
            rule.ToDate = (DateTime)request.ToDate;
            // rule.DaysOfWeek = request.DaysOfWeek != null ? string.Join(",", request.DaysOfWeek) : null;
            rule.Description = request.Description;

            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
