using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Dashboard
{ 
    public class BusinessDashboardDto
    {
        public int TotalCars { get; set; }
        public int AvailableCars { get; set; }
        public int ActiveReservations { get; set; }
        public int PendingReservations { get; set; }
        public int CanceledReservations { get; set; }
        public decimal Revenue { get; set; }
        public List<string> RecentReservations { get; set; } = new();
        public List<string> Notifications { get; set; } = new();
    }
}
