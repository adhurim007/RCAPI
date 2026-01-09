using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public DashboardCardsDto Cards { get; set; } = new();
        public List<MonthlyPointDto> ReservationsPerMonth { get; set; } = new();
        public List<MonthlyPointDto> IncomePerMonth { get; set; } = new();
    }

    public class DashboardCardsDto
    {
        public int TotalReservations { get; set; }
        public int ReservationsThisMonth { get; set; }
        public int TotalClients { get; set; }
        public int TotalCars { get; set; }
        public decimal IncomeThisMonth { get; set; }
        public int PendingReservations { get; set; } // opsionale
    }
    public class MonthlyPointDto
    {
        public int Year { get; set; }
        public int Month { get; set; } // 1-12
        public string Label { get; set; } = ""; // p.sh. "Jan 2026"
        public decimal Value { get; set; } // përdoret edhe për reservations edhe income
    }
}
