using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Dashboard
{
    public class AdminDashboardDto
    {
        public int TotalUsers { get; set; }
        public int TotalBusinesses { get; set; }
        public int TotalClients { get; set; }
        public int ActiveReservations { get; set; }
        public int CanceledReservations { get; set; }
        public int PendingReservations { get; set; }
        public decimal TotalRevenue { get; set; }
        public List<string> RecentAuditLogs { get; set; } = new();
        public List<string> RecentReservations { get; set; } = new();
    }

    public class AdminDashboardSummaryDto
    {
        public int TotalBusinesses { get; set; }
        public int TotalReservations { get; set; }
        public int TotalUsers { get; set; }
        public int TotalCars { get; set; }
    }
}
