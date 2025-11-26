using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Contract
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public string DocumentUrl { get; set; }   // ku ruhet PDF kontrata

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    } 
}
