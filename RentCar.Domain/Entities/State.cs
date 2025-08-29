using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // 🔗 One-to-Many
        public ICollection<City> Cities { get; set; } = new List<City>();
    }


    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int StateId { get; set; }
        public State State { get; set; }
    }
}
