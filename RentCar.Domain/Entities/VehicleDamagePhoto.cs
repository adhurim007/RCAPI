using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class VehicleDamagePhoto
    {
        public int Id { get; set; }

        public int VehicleDamageId { get; set; }
        public VehicleDamage VehicleDamage { get; set; }

        public string Url { get; set; }
    }
}
