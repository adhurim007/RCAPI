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
    public class DeleteCarPricingRuleHandler
           : IRequestHandler<DeleteCarPricingRuleCommand, bool>
    {
        private readonly ICarPricingRuleRepository _carPricingRepository;

        public DeleteCarPricingRuleHandler(ICarPricingRuleRepository carPricingRepository)
        {
            _carPricingRepository = carPricingRepository;
        }

        public async Task<bool> Handle(DeleteCarPricingRuleCommand request, CancellationToken cancellationToken)
        {
            var rule = await _carPricingRepository.GetAsync(request.Id);

            if (rule == null)
                return false;

            await _carPricingRepository.DeleteAsync(rule);
         
            return true;
        }
    }

}
