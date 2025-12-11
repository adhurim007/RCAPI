using MediatR;
using RentCar.Application.Common.Models;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.Cars.Queries.GetAllCars;
using RentCar.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class GetPagedCarsHandler : IRequestHandler<GetPagedCarsQuery, PaginatedResult<CarDto>>
    {
        private readonly ICarRepository _repo;

        public GetPagedCarsHandler(ICarRepository repo)
        {
            _repo = repo;
        }

        public async Task<PaginatedResult<CarDto>> Handle(GetPagedCarsQuery request, CancellationToken cancellationToken)
        {
            var query = _repo.GetQueryable();

           
            //if (!string.IsNullOrWhiteSpace(request.SortBy))
            //{
            //    query = request.IsDescending
            //        ? query.OrderByDescendingDynamic(request.SortBy)
            //        : query.OrderByDynamic(request.SortBy);
            //}

           
            var totalCount = await query.CountAsync(cancellationToken);

          
            var pagedData = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(car => new CarDto
                {
                    Id = car.Id,
                    DailyPrice = car.DailyPrice,
                    CarModelName = car.CarModel.Name,
                    CarBrandName = car.CarModel.CarBrand.Name, 
                    IsAvailable = car.IsAvailable
                })
                .ToListAsync(cancellationToken);

            return new PaginatedResult<CarDto>
            {
                TotalItems = totalCount,
                Items = pagedData
            };
        }

    }

}
