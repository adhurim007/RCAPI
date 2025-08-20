using MediatR;
using RentCar.Application.DTOs;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetCarImagesByCarIdQueryHandler : IRequestHandler<GetCarImagesByCarIdQuery, List<CarImageDto>>
    {
        private readonly ICarImageRepository _repo;

        public GetCarImagesByCarIdQueryHandler(ICarImageRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<CarImageDto>> Handle(GetCarImagesByCarIdQuery request, CancellationToken cancellationToken)
        {
            var images = await _repo.GetByCarIdAsync(request.CarId);
            return images.Select(i => new CarImageDto
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl
            }).ToList();
        }
    }

}
