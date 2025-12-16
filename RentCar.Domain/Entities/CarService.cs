using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class CarService
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        [Required]
        public CarServiceType ServiceType { get; set; }

        [Required]
        public DateTime ServiceDate { get; set; }

        // ✅ NEW
        public int BusinessId { get; set; }
        public Business Business { get; set; } = null!;

        public int? Mileage { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        [MaxLength(150)]
        public string? ServiceCenter { get; set; }

        public DateTime? NextServiceDate { get; set; }

        public int? NextServiceMileage { get; set; }

        public string? DocumentUrl { get; set; }

        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
