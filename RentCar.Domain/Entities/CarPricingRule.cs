using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class CarPricingRule
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public string RuleType { get; set; } = "Standard";
         
        public decimal PricePerDay { get; set; }
         
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
         
        public string? DaysOfWeek { get; set; }

        public string? Description { get; set; }
         
        public Car Car { get; set; } = null!;
    }


}
