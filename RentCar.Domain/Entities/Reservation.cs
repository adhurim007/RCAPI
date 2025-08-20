using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int ClientId { get; set; }
        public int BusinessId { get; set; }
        public int ReservationStatusId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }

        public Car Car { get; set; }
        public Client Client { get; set; }
        public Business Business { get; set; }
        public ReservationStatus ReservationStatus { get; set; }
        public Payment Payment { get; set; }
        public Contract Contract { get; set; }

        public ICollection<ReservationStatusHistory> StatusHistories { get; set; }
    }


}
