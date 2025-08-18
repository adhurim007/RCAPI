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
    }
}
