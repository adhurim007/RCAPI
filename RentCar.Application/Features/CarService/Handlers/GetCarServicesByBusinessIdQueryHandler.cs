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
    public class GetCarServicesByBusinessIdQueryHandler
        : IRequestHandler<GetCarServicesByBusinessIdQuery, List<CarServiceDto>>
    {
        private readonly RentCarDbContext _context;

        public GetCarServicesByBusinessIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarServiceDto>> Handle(
            GetCarServicesByBusinessIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.CarServices
                .Include(x => x.Car)
                .Where(x => x.Car.BusinessId == request.BusinessId)
                .OrderByDescending(x => x.ServiceDate)
                .Select(x => new CarServiceDto
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    ServiceType = (int)x.ServiceType,
                    ServiceDate = x.ServiceDate,
                    Mileage = x.Mileage,
                    Cost = x.Cost,
                    ServiceCenter = x.ServiceCenter,
                    NextServiceDate = x.NextServiceDate,
                    NextServiceMileage = x.NextServiceMileage,
                    Notes = x.Notes 
                })
                .ToListAsync(cancellationToken);
        }
    }
}
