using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RentCar.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

            services.AddAutoMapper(typeof(AssemblyMarker));  

            return services;
        }
    }
}
