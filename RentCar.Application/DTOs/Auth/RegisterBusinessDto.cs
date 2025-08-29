using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Auth
{
    public class RegisterBusinessDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Guid? UserId { get; set; }   // 👈 nullable
        public string? TaxId { get; set; }  // 👈 nullable

        public string CompanyName { get; set; }
        public string ContactPhone { get; set; }
        public int CityId { get; set; }
        public int StateId { get; set; }
    }


}
