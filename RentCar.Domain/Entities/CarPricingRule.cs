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

        public string RuleType { get; set; }
        public decimal Value { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }

        public Car Car { get; set; }
    }

}
