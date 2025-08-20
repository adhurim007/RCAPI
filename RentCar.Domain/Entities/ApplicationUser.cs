using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;


namespace RentCar.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Role { get; set; }
        public Client Client { get; set; }
        public Business Business { get; set; }
    }
}
