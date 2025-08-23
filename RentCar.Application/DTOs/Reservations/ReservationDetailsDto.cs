using RentCar.Application.DTOs.Contract;
using RentCar.Application.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Reservations
{
    public class ReservationDetailsDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public int BusinessId { get; set; }
        public string? CarModel { get; set; }
        public string? ClientName { get; set; }
        public string? BusinessName { get; set; }
        public string? Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        public List<PaymentDto> Payments { get; set; } = new();
        public ContractDto? Contract { get; set; }
        public List<ReservationStatusHistoryDto> StatusHistory { get; set; } = new();
    }
}
