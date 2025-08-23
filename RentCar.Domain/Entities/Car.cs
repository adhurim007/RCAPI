using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{

    public class Car
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public int CarModelId { get; set; }
        public int CarTypeId { get; set; }
        public int FuelTypeId { get; set; }
        public int TransmissionId { get; set; }

        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public decimal DailyPrice { get; set; }
        public bool IsAvailable { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public Business Business { get; set; }
        public CarModel CarModel { get; set; }
        public CarType CarType { get; set; }
        public FuelType FuelType { get; set; }
        public Transmission Transmission { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<CarPricingRule> PricingRules { get; set; }
        public ICollection<CarImage> Images { get; set; }
    }

}
