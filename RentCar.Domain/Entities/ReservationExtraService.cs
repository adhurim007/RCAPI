using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class ReservationExtraService
    {
        public int ReservationId { get; set; }
        public Reservation Reservation { get; set; }

        public int ExtraServiceId { get; set; }
        public ExtraService ExtraService { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
