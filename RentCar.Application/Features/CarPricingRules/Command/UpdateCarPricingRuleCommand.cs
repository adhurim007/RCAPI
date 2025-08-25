using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarPricingRules.Command
{
    public record UpdateCarPricingRuleCommand(
        int Id,
        string RuleType,
        decimal PricePerDay,
        DateTime? FromDate,
        DateTime? ToDate,
        List<DayOfWeek>? DaysOfWeek,
        string? Description
    ) : IRequest<bool>;


}
