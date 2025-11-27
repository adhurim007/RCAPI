using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class BusinessLocations
    {
        public int Id { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public string Name { get; set; }
        public string? Address { get; set; }

        public int StateId { get; set; }
        public State State { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
    }

}
