using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;   // e.g. "CreateCar", "DeleteReservation"
        public string EntityName { get; set; } = string.Empty; // e.g. "Car", "Reservation"
        public string EntityId { get; set; } = string.Empty;
        public string Changes { get; set; } = string.Empty; // JSON of what changed
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string IpAddress { get; set; } = string.Empty;
    }
}
