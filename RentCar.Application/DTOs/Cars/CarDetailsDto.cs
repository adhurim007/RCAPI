using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Cars
{
    public class CarDetailsDto
    {
        public int Id { get; set; }
        public string? LicensePlate { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public decimal DailyPrice { get; set; }

        public List<CarImageDto> Images { get; set; } = new();
        public List<CarServiceDto> Services { get; set; } = new();
        public List<CarRegistrationDto> Registrations { get; set; } = new();
    }
}
