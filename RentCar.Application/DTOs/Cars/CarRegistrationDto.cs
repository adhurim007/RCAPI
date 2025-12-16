using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Cars
{
    public class CarRegistrationDto
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public string? LicencePlate { get; set; }

        public string RegistrationNumber { get; set; } = null!;
        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public decimal Cost { get; set; }

        public string? InsuranceCompany { get; set; }
        public DateTime? InsuranceExpiryDate { get; set; }

        public string? Notes { get; set; }
        public string? DocumentUrl { get; set; }    
    }
}
