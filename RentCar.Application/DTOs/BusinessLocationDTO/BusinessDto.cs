using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.BusinessLocationDTO
{
    public class BusinessDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
