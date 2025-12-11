using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces.Repositories;  
using Microsoft.EntityFrameworkCore;

namespace RentCar.Persistence.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly RentCarDbContext _dbContext;

        public CarRepository(RentCarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _dbContext.Cars.ToListAsync();
        } 

        public async Task AddAsync(Car car)
        {
            await _dbContext.Cars.AddAsync(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Car car)
        {
            _dbContext.Cars.Update(car);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Car car)
        {
            _dbContext.Cars.Remove(car);
            await _dbContext.SaveChangesAsync();
        } 
        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbContext.Cars.AnyAsync(c => c.Id == id);
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _dbContext.Cars
                .Include(c => c.CarModel)
                    .ThenInclude(m => m.CarBrand)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public IQueryable<Car> Query()
        {
            return _dbContext.Cars.AsQueryable();
        }
         
        public async Task UpdateCarImageAsync(int carId, string imageUrl)
        {
            var car = await _dbContext.Cars.FindAsync(carId);
            if (car == null) throw new Exception("Car not found");

            car.ImageUrl = imageUrl;
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<Car> GetQueryable()
        {
            return _dbContext.Cars
                .Include(c => c.CarModel)
                    .ThenInclude(m => m.CarBrand); 
        }

        public Task<List<CarPricingRule>> GetByCarIdAsync(int carId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CarPricingRule>> GetPricingRulesAsync(int carId)
        {
            return await _dbContext.CarPricingRules.Where(r => r.CarId == carId).ToListAsync();
        }

        public async Task<CarPricingRule?> GetPricingRuleByIdAsync(int id)
        {
            return await _dbContext.CarPricingRules.FindAsync(id);
        }

        public async Task AddPricingRuleAsync(CarPricingRule rule)
        {
            await _dbContext.CarPricingRules.AddAsync(rule);
        }

        public async Task UpdatePricingRuleAsync(CarPricingRule rule)
        {
            _dbContext.CarPricingRules.Update(rule);
        }

        public async Task DeletePricingRuleAsync(int id)
        {
            var rule = await _dbContext.CarPricingRules.FindAsync(id);
            if (rule != null)
                _dbContext.CarPricingRules.Remove(rule);
        }
         
    }
}
