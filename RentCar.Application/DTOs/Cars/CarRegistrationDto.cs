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

        public string? LicensePlate { get; set; }
        public string RegistrationNumber { get; set; } = null!;

        public DateTime IssuedDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public decimal Cost { get; set; }

        public string? InsuranceCompany { get; set; }
        public DateTime? InsuranceExpiryDate { get; set; }

        public string? Notes { get; set; }
        public string? DocumentUrl { get; set; }

        public CarRegistrationDto(
         int id,
         int carId, 
         string registrationNumber,
         DateTime issuedDate,
         DateTime expiryDate,
         decimal cost,
         string? insuranceCompany,
         DateTime? insuranceExpiryDate,
         string? notes,
         string? documentUrl
     )
        {
            Id = id;
            CarId = carId; 
            RegistrationNumber = registrationNumber;
            IssuedDate = issuedDate;
            ExpiryDate = expiryDate;
            Cost = cost;
            InsuranceCompany = insuranceCompany;
            InsuranceExpiryDate = insuranceExpiryDate;
            Notes = notes;
            DocumentUrl = documentUrl;
        }

        public CarRegistrationDto() { }
    }


}
