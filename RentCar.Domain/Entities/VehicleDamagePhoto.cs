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

        public int DamageId { get; set; }
        public VehicleDamage Damage { get; set; }

        public string ImageUrl { get; set; }
    }
}
