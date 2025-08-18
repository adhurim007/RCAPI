using MediatR;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
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

            rule.FromDate = request.StartDate;
            rule.ToDate = request.EndDate;
            rule.Value = request.Price;

            _unitOfWork.CarPricingRules.Update(rule);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

}
