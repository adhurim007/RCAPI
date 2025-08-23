using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Reservations
{
    public class ReservationStatusHistoryDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public string? Status { get; set; }
        public int ReservationStatusId { get; set; }
        public DateTime ChangedAt { get; set; }
        public string? ReservationStatusName { get; set; } 
        public string? ChangedBy { get; set; }
        public string? Note { get; set; }
    }

}
