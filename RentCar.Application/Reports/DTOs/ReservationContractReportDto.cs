using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.DTOs
{
    public class ReservationContractReportDto
    {
        // General
        public string ContractNumber { get; set; } = string.Empty;
        public DateTime GeneratedAt { get; set; }

        // Business
        public string BusinessName { get; set; } = string.Empty;
        public string BusinessAddress { get; set; } = string.Empty;

        // Client
        public string ClientFullName { get; set; } = string.Empty;
        public string ClientPersonalNumber { get; set; } = string.Empty;

        // Car
        public string CarName { get; set; } = string.Empty;
        public string PlateNumber { get; set; } = string.Empty;

        // Reservation
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalCost { get; set; }

        // Contract text (template-generated)
        public string ContractText { get; set; } = string.Empty;
    }
}
