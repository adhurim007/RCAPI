using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentCarDbContext _context;

        public ICarPricingRuleRepository CarPricingRules { get; }

        public UnitOfWork(RentCarDbContext context, ICarPricingRuleRepository carPricingRuleRepository)
        {
            _context = context;
            CarPricingRules = carPricingRuleRepository;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync();
        } 
    } 
}
