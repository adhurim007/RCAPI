using MediatR;
using RentCar.Application.DTOs;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Entities;
using RentCar.Domain.Interfaces.Repositories;
using RentCar.Infrastructure;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class UploadCarImageCommandHandler : IRequestHandler<UploadCarImageCommand, List<CarImageDto>>
    {
        private readonly ICarImageRepository _repo;
        private readonly IFileStorageService _storage;

        public UploadCarImageCommandHandler(ICarImageRepository repo, IFileStorageService storage)
        {
            _repo = repo;
            _storage = storage;
        }

        public async Task<List<CarImageDto>> Handle(UploadCarImageCommand request, CancellationToken cancellationToken)
        {
            var result = new List<CarImageDto>();

            foreach (var file in request.Files)
            {
                var imagePath = await _storage.SaveImageAsync(file, "uploads/cars");
                var image = new CarImage
                { 
                    CarId = request.CarId,
                    ImageUrl = imagePath,
                    UploadedAt = DateTime.UtcNow
                };

                await _repo.AddAsync(image);
                result.Add(new CarImageDto { Id = image.Id, ImageUrl = image.ImageUrl });
            }

            return result;
        }
    }

}
