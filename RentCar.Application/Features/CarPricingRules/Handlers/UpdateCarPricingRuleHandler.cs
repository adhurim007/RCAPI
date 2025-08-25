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
    public class UpdateCarPricingRuleHandler : IRequestHandler<UpdateCarPricingRuleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCarPricingRuleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _unitOfWork.CarPricingRules.GetByIdAsync(request.Id);
            if (rule == null) return false;

            rule.FromDate = (DateTime)request.FromDate;
            rule.ToDate = (DateTime)request.ToDate;
            rule.Value = request.PricePerDay;

            _unitOfWork.CarPricingRules.Update(rule);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

}
