using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces.Repositories; 

namespace RentCar.Persistence.Repositories
{
    public class CarImageRepository : ICarImageRepository
    {
        private readonly RentCarDbContext _context;

        public CarImageRepository(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(CarImage image)
        {
            _context.CarImages.Add(image);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CarImage>> GetByCarIdAsync(int carId)
        {
            return await _context.CarImages
                .Where(i => i.CarId == carId)
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(CarImage id)
        {
            var image = await _context.CarImages.FindAsync(id);
            if (image == null) return false;

            _context.CarImages.Remove(image);
            await _context.SaveChangesAsync();
            return true;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CarImage?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task ICarImageRepository.DeleteAsync(CarImage image)
        {
            return DeleteAsync(image);
        }

        public Task UpdateAsync(object existingImage)
        {
            throw new NotImplementedException();
        }
    }

}
