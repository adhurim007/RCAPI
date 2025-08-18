using Microsoft.Extensions.DependencyInjection; 
using RentCar.Domain.Interfaces.Repositories;
using RentCar.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICarRepository, CarRepository>();

            return services;
        }
    }
}
