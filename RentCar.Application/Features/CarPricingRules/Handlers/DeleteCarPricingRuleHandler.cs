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
    public class DeleteCarPricingRuleHandler : IRequestHandler<DeleteCarPricingRuleCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCarPricingRuleHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _unitOfWork.CarPricingRules.GetByIdAsync(request.Id);
            if (rule == null) return false;

            _unitOfWork.CarPricingRules.Delete(rule);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }

}
