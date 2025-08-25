using MediatR;
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
    public class DeleteCarImageCommandHandler : IRequestHandler<DeleteCarImageCommand, bool>
    {
        private readonly ICarImageRepository _repo;
        private readonly IFileStorageService _storage;

        public DeleteCarImageCommandHandler(ICarImageRepository repo, IFileStorageService storage)
        {
            _repo = repo;
            _storage = storage;
        }

        public async Task<bool> Handle(DeleteCarImageCommand request, CancellationToken cancellationToken)
        {
            var image = await _repo.GetByIdAsync(request.ImageId);
            if (image == null) return false;

            await _storage.DeleteImageAsync(image.ImageUrl);
            await _repo.DeleteAsync(image);
            return true;
        }
    }


}
