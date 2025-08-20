using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Cars
{
    public class CarPricingRuleDto
    {
        public int Id { get; set; }
        public int CarId { get; set; } 
        public int MinDays { get; set; }
        public int MaxDays { get; set; } 
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PricePerDay { get; set; }
    } 
}
