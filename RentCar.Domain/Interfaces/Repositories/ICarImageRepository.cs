using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces.Repositories
{
    public interface ICarImageRepository
    {
        Task AddAsync(CarImage image);
        Task<List<CarImage>> GetByCarIdAsync(int carId); 
        Task<bool> DeleteAsync(int id); 
        Task<CarImage?> GetByIdAsync(int id);
        Task DeleteAsync(CarImage image);
        Task UpdateAsync(object existingImage);
    }
}
