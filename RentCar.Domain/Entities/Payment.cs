﻿using System;
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

        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; }
        public string PaymentMethod { get; set; }

        public Reservation Reservation { get; set; }
    }
}
