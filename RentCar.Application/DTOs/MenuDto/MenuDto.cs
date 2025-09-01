using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.MenuDto
{
    public class MenuDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Type { get; set; } = "basic";  
        public string Icon { get; set; }
        public string Link { get; set; }   
    }
}
