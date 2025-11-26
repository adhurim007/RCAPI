using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Reservation> PickupReservations { get; set; }
            = new List<Reservation>();

        public ICollection<Reservation> DropoffReservations { get; set; }
            = new List<Reservation>();
    }

}
