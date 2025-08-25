using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Dashboard
{
    public class MonthlyIncomeDto
    {
        public string Month { get; set; } = string.Empty;
        public decimal Income { get; set; }
    }
}
