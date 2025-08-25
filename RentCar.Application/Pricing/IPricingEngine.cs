using Azure.Core;
using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;
using RentCar.Persistence;

namespace RentCar.Application.Pricing
{
    public interface IPricingEngine
    {
        Task<decimal> CalculateReservationPrice(int carId, DateTime startDate, DateTime endDate);
    }

    public class PricingEngine : IPricingEngine
    {
        private readonly RentCarDbContext _context;

        public PricingEngine(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> CalculateReservationPrice(int carId, DateTime startDate, DateTime endDate)
        {
            var car = await _context.Cars.Include(c => c.PricingRules).FirstOrDefaultAsync(c => c.Id == carId);
            if (car == null) throw new Exception("Car not found");

            decimal totalPrice = 0;
            var currentDate = startDate.Date;
             
            while (currentDate < endDate)
            {
                decimal dayPrice = car.DailyPrice; // default

                var rules = car.PricingRules
                    .Where(r =>
                        (!r.FromDate.HasValue || currentDate >= r.FromDate.Value.Date) &&
                        (!r.ToDate.HasValue || currentDate <= r.ToDate.Value.Date)
                    )
                    .ToList();

                foreach (var rule in rules)
                {
                    if (rule.RuleType == "Weekend" && !string.IsNullOrEmpty(rule.DaysOfWeek))
                    {
                        var days = rule.DaysOfWeek.Split(',');   // ✅ array of strings
                        if (days.Contains(currentDate.DayOfWeek.ToString()))
                        {
                            dayPrice = rule.PricePerDay;
                        }
                    }
                    else if (rule.RuleType == "Seasonal" || rule.RuleType == "Custom")
                    {
                        dayPrice = rule.PricePerDay;
                    }
                }

                totalPrice += dayPrice;
                currentDate = currentDate.AddDays(1);
            } 

            return totalPrice;
        }
    }
}
