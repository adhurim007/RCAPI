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
    public class CarPricingRuleRepository : GenericRepository<CarPricingRule>, ICarPricingRuleRepository
    {
        public CarPricingRuleRepository(RentCarDbContext context) : base(context)
        {
        }
         
        public Task<CarPricingRule?> GetByCarIdAsync(Guid carId)
        {
            return _context.CarPricingRules.FirstOrDefaultAsync(x => x.Id == carId);
        }
    } 
     
}
