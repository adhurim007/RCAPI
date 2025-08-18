using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RentCar.Domain.Interfaces
{
    public interface IUnitOfWork
    { 
        ICarPricingRuleRepository CarPricingRules { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }

}
