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

        public string FileUrl { get; set; }
        public DateTime CreatedAt { get; set; }

        public Reservation Reservation { get; set; }
    }
}
