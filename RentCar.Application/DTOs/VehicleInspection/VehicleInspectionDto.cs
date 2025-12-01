using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.VehicleInspection
{
    public class VehicleInspectionDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int Type { get; set; }
        public int Mileage { get; set; }
        public decimal FuelLevel { get; set; }
        public string? TireCondition { get; set; }
        public string? OverallCondition { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<string> Photos { get; set; } = new();
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string Customer { get; set; }
    }

}
