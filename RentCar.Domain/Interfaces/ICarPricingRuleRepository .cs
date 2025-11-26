using RentCar.Domain.Entities;
using System;
using System.Collections.Generic; 
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
    public interface ICarPricingRuleRepository 
    {
        Task AddAsync(CarPricingRule car);
        Task DeleteAsync(CarPricingRule car);
        Task<List<CarPricingRule>> GetByCarIdAsync(int carId);

        Task<IEnumerable<CarPricingRule>> GetAllAsync();

        Task<CarPricingRule> GetAsync(int id);
    }
}
