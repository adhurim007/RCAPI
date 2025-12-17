using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Cars
{
    public class CarImageDto {
        public int Id { get; set; } 
        public string ImageUrl { get; set;}

        public CarImageDto(int id, string imageUrl)
        {
            Id = id;
            ImageUrl = imageUrl;
        }

        public CarImageDto() { }
    }
        
}
