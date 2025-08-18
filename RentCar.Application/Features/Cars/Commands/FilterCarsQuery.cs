using MediatR;
using RentCar.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public class FilterCarsQuery : IRequest<List<CarDto>>
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public int? FuelTypeId { get; set; }
        public int? TransmissionId { get; set; }
        public bool? IsAvailable { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }

}
