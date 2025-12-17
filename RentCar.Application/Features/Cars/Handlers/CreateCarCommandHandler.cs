using MediatR;
using Microsoft.AspNetCore.Hosting;
using RentCar.Application.Auditing;
using RentCar.Application.Features.Cars.Commands; 
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces.Repositories;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class CreateCarCommandHandler
   : IRequestHandler<CreateCarCommand, int>
    {
        private readonly ICarRepository _carRepository;
        private readonly IAuditLogService _auditLogService;
        private readonly RentCarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateCarCommandHandler(
            ICarRepository carRepository,
            IAuditLogService auditLogService,
            RentCarDbContext context,
            IWebHostEnvironment env)
        {
            _carRepository = carRepository;
            _auditLogService = auditLogService;
            _context = context;
            _env = env;
        }

        public async Task<int> Handle(
            CreateCarCommand request,
            CancellationToken cancellationToken)
        {
             
            var car = new Car
            {
                BusinessId = request.BusinessId,
                CarBrandId = request.CarBrandId,
                CarModelId = request.CarModelId,
                CarTypeId = request.CarTypeId,
                FuelTypeId = request.FuelTypeId,
                TransmissionId = request.TransmissionId,
                LicensePlate = request.LicensePlate,
                Color = request.Color,
                DailyPrice = request.DailyPrice,
                Description = request.Description,
                IsAvailable = request.IsAvailable,
                CreatedAt = DateTime.UtcNow
            };

            await _carRepository.AddAsync(car);
 
            if (request.Images != null && request.Images.Any())
            {
                var uploadPath = Path.Combine(
                    _env.WebRootPath,
                    "uploads",
                    "cars"
                );

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                foreach (var file in request.Images)
                {
                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                    var fullPath = Path.Combine(uploadPath, fileName);

                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await file.CopyToAsync(stream, cancellationToken);

                    _context.CarImages.Add(new CarImage
                    {
                        CarId = car.Id,
                        ImageUrl = $"/uploads/cars/{fileName}",
                        UploadedAt = DateTime.UtcNow
                    });
                }

                await _context.SaveChangesAsync(cancellationToken);
            }
             
            //await _auditLogService.LogAsync(
            //    "Create",
            //    "Car",
            //    car.Id.ToString(),
            //    car
            //);

            return car.Id;
        }
    }

}
