using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class BusinessLocation
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public int LocationId { get; set; }

        public Business Business { get; set; }
        public Location Location { get; set; }
    }

}
