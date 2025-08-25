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
    public class DeleteCarPricingRuleCommandHandler : IRequestHandler<DeleteCarPricingRuleCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public DeleteCarPricingRuleCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _context.CarPricingRules.FindAsync(request.Id);
            if (rule == null) return false;

            _context.CarPricingRules.Remove(rule);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
    }

}
