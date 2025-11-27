using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Repositories
{
    public class CarPricingRuleRepository : ICarPricingRuleRepository
    {
        private readonly RentCarDbContext _dbContext;

        public CarPricingRuleRepository(RentCarDbContext dbContext)  
        {
            _dbContext = dbContext;
        } 
        public async Task<List<CarPricingRule>> GetByCarIdAsync(int carId)
        {
            return await _dbContext.CarPricingRules
                .Where(r => r.CarId == carId)
                .ToListAsync();
        }

        public async Task AddAsync(CarPricingRule car)
        {
            await _dbContext.CarPricingRules.AddAsync(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(CarPricingRule car)
        {
            _dbContext.CarPricingRules.Remove(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CarPricingRule> GetAsync(int id)
        {
            return await _dbContext.CarPricingRules.FirstOrDefaultAsync(r => r.Id == id);
        }
         
        public async Task<IEnumerable<CarPricingRule>> GetAllAsync()
        {
            return await _dbContext.CarPricingRules.ToListAsync();
        }
         
    }

}
