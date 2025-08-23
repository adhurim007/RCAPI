using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class CarImage
    {
        public int Id { get; set; }
        public int CarId { get; set; }

        public string ImageUrl { get; set; }
        public DateTime UploadedAt { get; set; }

        public Car Car { get; set; }
    }
}
 
