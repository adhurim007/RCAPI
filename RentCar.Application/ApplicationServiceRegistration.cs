using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RentCar.Application.Reports.Abstractions;
using RentCar.Application.Reports.Datasets;
using RentCar.Application.Reports.Implementations; 
using RentCar.Application.Reports.Services;

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
