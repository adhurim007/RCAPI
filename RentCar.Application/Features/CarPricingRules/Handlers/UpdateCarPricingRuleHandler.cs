using MediatR;
using RentCar.Application.Features.CarPricingRules.Command;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Handlers
{
    public class UpdateCarPricingRuleHandler
        : IRequestHandler<UpdateCarPricingRuleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarPricingRuleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _unitOfWork.CarPricingRules.GetAsync(request.Id);

            if (rule == null)
                return false;

            rule.CarId = request.CarId;
            rule.RuleType = request.RuleType;
            rule.PricePerDay = request.PricePerDay;
            rule.FromDate = request.FromDate;
            rule.ToDate = request.ToDate;
            rule.DaysOfWeek = request.DaysOfWeek != null
                ? string.Join(",", request.DaysOfWeek)
                : null;
            rule.Description = request.Description;

            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

}
