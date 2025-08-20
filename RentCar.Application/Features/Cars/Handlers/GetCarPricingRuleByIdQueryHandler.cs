using MediatR;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarPricingRuleByIdQueryHandler : IRequestHandler<GetCarPricingRuleByIdQuery, CarPricingRuleDto?>
    {
        private readonly ICarPricingRuleRepository _repository;

        public GetCarPricingRuleByIdQueryHandler(ICarPricingRuleRepository repository)
        {
            _repository = repository;
        }

        public async Task<CarPricingRuleDto?> Handle(GetCarPricingRuleByIdQuery request, CancellationToken cancellationToken)
        {
            var rule = await _repository.GetByIdAsync(request.Id);
            if (rule == null) return null;

            return new CarPricingRuleDto
            {
                Id = rule.Id,
                CarId = rule.CarId,
                StartDate = rule.FromDate,
                EndDate = rule.ToDate = rule.ToDate,  
            };
        }
    }
}
