using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Reservations
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public string? CarModel { get; set; }
        public string? CarBrand { get; set; }
        public string? LicensePlate { get; set; }
        public int ClientId { get; set; }
        public string? ClientName { get; set; }
        public int BusinessId { get; set; }
        public string? BusinessName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
    }
}
