using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }
        public Guid ClientId { get; set; }
        public Guid BusinessId { get; set; }
        public int ReservationStatusId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        // ✅ Navigation Properties
        public Car Car { get; set; }
        public Client Client { get; set; }
        public Business Business { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public Payment Payment { get; set; }
        public Contract Contract { get; set; }
    }

}
