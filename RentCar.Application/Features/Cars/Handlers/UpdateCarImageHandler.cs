using MediatR;
using RentCar.Application.Features.Cars.Commands;
using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class UpdateCarImageHandler : IRequestHandler<UpdateCarImageCommand>
    {
        private readonly ICarRepository _carRepo;

        public UpdateCarImageHandler(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        public async Task<Unit> Handle(UpdateCarImageCommand request, CancellationToken cancellationToken)
        {
            await _carRepo.UpdateCarImageAsync(request.CarId, request.ImageUrl);
            return Unit.Value;
        }

        Task IRequestHandler<UpdateCarImageCommand>.Handle(UpdateCarImageCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }

}
