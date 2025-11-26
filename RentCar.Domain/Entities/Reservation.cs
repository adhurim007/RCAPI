using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public string ReservationNumber { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int CarId { get; set; }
        public Car Car { get; set; }

        public int PickupLocationId { get; set; }
        public Location PickupLocation { get; set; }

        public int DropoffLocationId { get; set; }
        public Location DropoffLocation { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public int TotalDays { get; set; }

        public decimal BasePricePerDay { get; set; }
        public decimal TotalPrice { get; set; }

        public decimal? DepositAmount { get; set; }
        public DepositStatus DepositStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
        public ReservationStatus ReservationStatus { get; set; }

        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // RELATIONS
        public ICollection<ReservationExtraService> ExtraServices { get; set; } = new List<ReservationExtraService>();
        public ICollection<AdditionalDriver> AdditionalDrivers { get; set; }
        public ICollection<VehicleInspection> Inspections { get; set; }
        public ICollection<VehicleDamage> Damages { get; set; }
        public ICollection<Payment> Payments { get; set; }

        public Contract Contract { get; set; }
    }



}
