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

        public int ReservationId { get; set; }   // FK
        public Reservation Reservation { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public InspectionType Type { get; set; }
        public int Mileage { get; set; }
        public decimal FuelLevel { get; set; }
        public string? TireCondition { get; set; }
        public string? OverallCondition { get; set; }

        public ICollection<VehicleInspectionPhoto> Photos { get; set; }
            = new List<VehicleInspectionPhoto>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
