using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.BusinessLocationDTO
{
    public class BusinessLocationDto
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }

        public string Name { get; set; }
        public string? Address { get; set; }

        public int StateId { get; set; }
        public string StateName { get; set; }

        public int CityId { get; set; }
        public string CityName { get; set; }
    }

}
