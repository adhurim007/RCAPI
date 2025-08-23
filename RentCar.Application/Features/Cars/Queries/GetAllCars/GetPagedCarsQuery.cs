using MediatR;
using RentCar.Application.Common.Models;
using RentCar.Application.DTOs.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Queries.GetAllCars
{
    public class GetPagedCarsQuery : IRequest<PaginatedResult<CarDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? SortBy { get; set; }  
        public bool IsDescending { get; set; }
    } 
}
