using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class ExtraService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PricePerDay { get; set; }

        public ICollection<ReservationExtraService> ReservationExtraServices { get; set; }
            = new List<ReservationExtraService>();
    }

}
