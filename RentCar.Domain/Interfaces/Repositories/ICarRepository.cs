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
        Task<bool> ExistsAsync(int id);
        Task<Car> GetByIdAsync(int id);
        IQueryable<Car> Query();
        Task UpdateCarImageAsync(int carId, string imageUrl);
        Task<List<CarPricingRule>> GetByCarIdAsync(int carId); 
        IQueryable<Car> GetQueryable();

        Task<List<CarPricingRule>> GetPricingRulesAsync(int carId);
        Task<CarPricingRule?> GetPricingRuleByIdAsync(int id);
        Task AddPricingRuleAsync(CarPricingRule rule);
        Task UpdatePricingRuleAsync(CarPricingRule rule);
        Task DeletePricingRuleAsync(int id);
    }
}
