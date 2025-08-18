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
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public string RuleType { get; set; } // e.g., Weekend, Seasonal
        public decimal Value { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }

        public Car Car { get; set; }
    }
}
