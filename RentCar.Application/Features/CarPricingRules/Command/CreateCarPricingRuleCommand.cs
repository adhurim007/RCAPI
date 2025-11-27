using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Command
{
    public class CreateCarPricingRuleCommand : IRequest<int>
    {
        public int CarId { get; set; }

        public string RuleType { get; set; } = string.Empty;

        public decimal PricePerDay { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public List<string> DaysOfWeek { get; set; } = new();

        public string? Description { get; set; }
    }

}
