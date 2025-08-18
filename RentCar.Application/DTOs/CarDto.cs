using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs
{
    public class CarDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public decimal DailyPrice { get; set; }
        public string Description { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public string CarModelName { get; set; }
        public string CarBrandName { get; set; }
        public string CarTypeName { get; set; }
        public string FuelTypeName { get; set; }
        public string TransmissionName { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
          
        public int Year { get; set; }
        

    }
}
