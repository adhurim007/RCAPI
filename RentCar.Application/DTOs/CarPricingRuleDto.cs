using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs
{
    public class CarPricingRuleDto
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public decimal PricePerDay { get; set; }
        public int MinDays { get; set; }
        public int MaxDays { get; set; }
    } 
}
