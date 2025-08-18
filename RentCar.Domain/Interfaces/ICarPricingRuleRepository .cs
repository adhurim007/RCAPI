using RentCar.Domain.Entities;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
    public interface ICarPricingRuleRepository : IGenericRepository<CarPricingRule>
    {
        Task<CarPricingRule> GetByCarIdAsync(Guid carId);
        Task<CarPricingRule> GetByIdAsync(Guid id);
    }
}
