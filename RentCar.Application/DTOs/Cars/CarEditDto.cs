using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Cars
{
    public class CarEditDto
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }

        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public int CarTypeId { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionId { get; set; }

        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }

        public List<string> Images { get; set; }
    }
}
