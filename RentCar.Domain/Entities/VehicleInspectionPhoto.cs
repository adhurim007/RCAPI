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

        public int InspectionId { get; set; }
        public VehicleInspection Inspection { get; set; }

        public string ImageUrl { get; set; }
    }
}
