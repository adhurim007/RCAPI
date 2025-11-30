using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class VehicleInspection
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public InspectionType Type { get; set; }  

        public int Mileage { get; set; }
        public decimal FuelLevel { get; set; }    
        public string TireCondition { get; set; }
        public string OverallCondition { get; set; }

        public List<VehicleInspectionPhoto> Photos { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
