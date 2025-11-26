using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class VehicleInspectionPhoto
    {
        public int Id { get; set; }

        public int VehicleInspectionId { get; set; }
        public VehicleInspection VehicleInspection { get; set; }

        public string Url { get; set; }
    }
}
