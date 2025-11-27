using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Business
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string CompanyName { get; set; }
        public string TaxId { get; set; }
        public string ContactPhone { get; set; }
        public bool IsApproved { get; set; }
        public bool IsActive { get; set; }
        public int? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; } 

        // 🔗 Location info
        public int StateId { get; set; }
        public State State { get; set; }
        public string Address { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }
        

        public ApplicationUser User { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<BusinessLocations> Locations { get; set; }
    }



}
