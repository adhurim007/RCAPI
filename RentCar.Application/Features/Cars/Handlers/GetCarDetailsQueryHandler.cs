using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarDetailsQueryHandler
    : IRequestHandler<GetCarDetailsQuery, CarDetailsDto?>
    {
        private readonly RentCarDbContext _context;

        public GetCarDetailsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<CarDetailsDto?> Handle(
            GetCarDetailsQuery request,
            CancellationToken cancellationToken)
        {
            var car = await _context.Cars
                .Include(x => x.CarBrand)
                .Include(x => x.CarModel)
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (car == null)
                return null;

            return new CarDetailsDto
            {
                Id = car.Id,
                LicensePlate = car.LicensePlate,
                Brand = car.CarBrand?.Name,
                Model = car.CarModel?.Name,
                Color = car.Color,
                DailyPrice = car.DailyPrice,

                Images = await _context.CarImages
                    .Where(i => i.CarId == car.Id)
                    .Select(i => new CarImageDto
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl!
                    })
                    .ToListAsync(cancellationToken),

                Services = await _context.CarServices
                    .Where(s => s.CarId == car.Id)
                    .OrderByDescending(s => s.ServiceDate)
                    .Select(s => new CarServiceDto(
                        s.Id,
                        s.CarId,
                        s.BusinessId,
                        (int)s.ServiceType,
                        s.ServiceDate,
                        s.Mileage,
                        s.Cost,
                        s.ServiceCenter,
                        s.NextServiceDate,
                        s.NextServiceMileage,
                        s.Notes
                    ))
                    .ToListAsync(cancellationToken),

                Registrations = await _context.CarRegistrations
                    .Where(r => r.CarId == car.Id)
                    .OrderByDescending(r => r.ExpiryDate)
                    .Select(r => new CarRegistrationDto(
                        r.Id,
                        r.CarId, 
                        r.RegistrationNumber,
                        r.IssuedDate,
                        r.ExpiryDate,
                        r.Cost,
                        r.InsuranceCompany,
                        r.InsuranceExpiryDate,
                        r.Notes,
                        r.DocumentUrl
                    ))
                    .ToListAsync(cancellationToken)
            };
        } 
      }
    }
