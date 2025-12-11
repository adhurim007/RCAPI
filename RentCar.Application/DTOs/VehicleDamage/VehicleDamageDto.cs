using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.VehicleDamage
{
    public class VehicleDamageDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string? DamageType { get; set; }
        public string? Description { get; set; }
        public decimal EstimatedCost { get; set; }
        public DamageStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<string> Photos { get; set; } = new();
    }
}
