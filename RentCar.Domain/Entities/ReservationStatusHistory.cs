using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class ReservationStatusHistory
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public int ReservationStatusId { get; set; }
        public DateTime ChangedAt { get; set; }
        public string ChangedBy { get; set; }  
        public string Note { get; set; }

        public Reservation Reservation { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
    }

}
