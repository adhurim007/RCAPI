using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class CarRegistration
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CarId { get; set; }

        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;
        // ✅ NEW
        public int BusinessId { get; set; }
        public Business Business { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string RegistrationNumber { get; set; } = null!;

        [Required]
        public DateTime IssuedDate { get; set; }

        [Required]
        public DateTime ExpiryDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Cost { get; set; }

        [MaxLength(150)]
        public string? InsuranceCompany { get; set; }

        public DateTime? InsuranceExpiryDate { get; set; }

        public string? DocumentUrl { get; set; }

        public string? Notes { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
