using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class CarType // SUV, Sedan, etc.
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
