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
        public string RuleType { get; set; } = string.Empty;
        public decimal PricePerDay { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; } 
        public string? DaysOfWeek { get; set; }

        public string? Description { get; set; }
    }
}
