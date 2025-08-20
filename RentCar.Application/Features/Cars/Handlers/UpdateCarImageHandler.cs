using MediatR;
using RentCar.Application.DTOs;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Interfaces.Repositories;
using RentCar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class UpdateCarImageCommandHandler : IRequestHandler<UpdateCarImageCommand, CarImageDto>
    {
        private readonly ICarImageRepository _repo;
        private readonly IFileStorageService _storage;

        public UpdateCarImageCommandHandler(ICarImageRepository repo, IFileStorageService storage)
        {
            _repo = repo;
            _storage = storage;
        }

        public async Task<CarImageDto> Handle(UpdateCarImageCommand request, CancellationToken cancellationToken)
        {
            var existingImage = await _repo.GetByIdAsync(request.ImageId);
            if (existingImage == null)
                throw new KeyNotFoundException("Image not found");

            
            await _storage.DeleteImageAsync(existingImage.ImageUrl);

           
            var newImagePath = await _storage.SaveImageAsync(request.NewImage, "uploads/cars");

        
            existingImage.ImageUrl = newImagePath;
            existingImage.UploadedAt = DateTime.UtcNow;

            await _repo.UpdateAsync(existingImage);

            return new CarImageDto
            {
                Id = existingImage.Id,
                ImageUrl = existingImage.ImageUrl
            };
        }
    } 
}
