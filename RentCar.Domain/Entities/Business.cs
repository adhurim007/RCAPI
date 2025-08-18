using System;
using System.Collections.Generic;
using System.Linq;
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
        public Guid? ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<Car> Cars { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<BusinessLocation> Locations { get; set; }
    }


}
