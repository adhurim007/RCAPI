using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class UploadCarImagesCommandHandler
    : IRequestHandler<UploadCarImagesCommand>
    {
        private readonly RentCarDbContext _context;
        private readonly IWebHostEnvironment _env;

        public UploadCarImagesCommandHandler(
            RentCarDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task Handle(
            UploadCarImagesCommand request,
            CancellationToken cancellationToken)
        {
            if (request.Files == null || request.Files.Count == 0)
                return;

            var car = await _context.Cars
                .Include(x => x.Images)
                .FirstOrDefaultAsync(x => x.Id == request.CarId, cancellationToken);

            if (car == null)
                throw new Exception("Car not found");

            var uploadPath = Path.Combine(
                _env.WebRootPath,
                "uploads",
                "cars"
            );

            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            foreach (var file in request.Files)
            {
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var fullPath = Path.Combine(uploadPath, fileName);

                using var stream = new FileStream(fullPath, FileMode.Create);
                await file.CopyToAsync(stream, cancellationToken);

                car.Images.Add(new CarImage
                {
                    ImageUrl = $"/uploads/cars/{fileName}",
                    UploadedAt = DateTime.UtcNow
                });
            }

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
