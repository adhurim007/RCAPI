using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Dashboard
{
    public class MonthlyStatDto
    {
        public string Month { get; set; } = string.Empty; // e.g. "2025-01"
        public int Count { get; set; }
    }
}
