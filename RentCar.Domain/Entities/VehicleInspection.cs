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

        public InspectionType InspectionType { get; set; }

        public decimal FuelLevel { get; set; } // 0–1 or liters
        public int Mileage { get; set; }
        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<VehicleInspectionPhoto> Photos { get; set; }
    }
}
