using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public decimal Amount { get; set; }
        public string Method { get; set; } // Cash, Card, Bank Transfer
        public PaymentStatus Status { get; set; }
        public DateTime PaidAt { get; set; }
    }

}
