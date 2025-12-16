using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Cars
{
    public class CarServiceDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int BusinessId { get; set; }
        public int ServiceType { get; set; }
        public DateTime ServiceDate { get; set; }
        public int? Mileage { get; set; }
        public decimal Cost { get; set; }
        public string? ServiceCenter { get; set; }
        public DateTime? NextServiceDate { get; set; }
        public int? NextServiceMileage { get; set; }
        public string? Notes { get; set; }
    }
}
