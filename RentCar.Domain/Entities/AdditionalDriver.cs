using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class AdditionalDriver
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public string FullName { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
