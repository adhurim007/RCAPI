using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.CarService.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarService.Handlers
{
    public class GetCarServiceByIdQueryHandler
        : IRequestHandler<GetCarServiceByIdQuery, CarServiceDto?>
    {
        private readonly RentCarDbContext _context;

        public GetCarServiceByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<CarServiceDto?> Handle(
            GetCarServiceByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.CarServices
                .Where(x => x.Id == request.Id)
                .Select(x => new CarServiceDto
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    BusinessId = x.BusinessId,
                    ServiceType = (int)x.ServiceType,
                    ServiceDate = x.ServiceDate,
                    Mileage = x.Mileage,
                    Cost = x.Cost,
                    ServiceCenter = x.ServiceCenter,
                    NextServiceDate = x.NextServiceDate,
                    NextServiceMileage = x.NextServiceMileage,
                    Notes = x.Notes
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
