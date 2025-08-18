using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces.Repositories
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync(); 
        Task AddAsync(Car car);
        Task UpdateAsync(Car car);
        Task DeleteAsync(Car car);
        Task<bool> ExistsAsync(Guid id);
        Task<Car> GetByIdAsync(Guid id);
        IQueryable<Car> Query();
        Task UpdateCarImageAsync(Guid carId, string imageUrl);
        Task<List<CarPricingRule>> GetByCarIdAsync(Guid carId); 
        IQueryable<Car> GetQueryable();

        Task<List<CarPricingRule>> GetPricingRulesAsync(Guid carId);
        Task<CarPricingRule?> GetPricingRuleByIdAsync(Guid id);
        Task AddPricingRuleAsync(CarPricingRule rule);
        Task UpdatePricingRuleAsync(CarPricingRule rule);
        Task DeletePricingRuleAsync(Guid id);
    }
}
