using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class VehicleDamage
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public string DamageType { get; set; }
        public string Description { get; set; }
        public decimal EstimatedCost { get; set; }

        public DamageStatus Status { get; set; } = DamageStatus.Pending;

        public List<VehicleDamagePhoto> Photos { get; set; } = new();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
