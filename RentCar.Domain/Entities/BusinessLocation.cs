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
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
