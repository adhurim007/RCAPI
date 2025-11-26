using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Commands
{
    public class CreateCarCommand : IRequest<int>
    {
        public int BusinessId { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public int CarTypeId { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionId { get; set; }
        public string LicensePlate { get; set; } = default!;
        public string Color { get; set; } = default!;
        public decimal DailyPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; } = default!;
        public bool IsAvailable { get; set; }
    }
}
