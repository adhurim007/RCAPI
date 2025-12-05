using RentCar.Application.DTOs.Contract;
using RentCar.Application.DTOs.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.DTOs.Reservations
{
    public class ReservationDetailsDto
    {
        public int Id { get; set; }
        public string ReservationNumber { get; set; }

        public int CarId { get; set; }
        public string CarBrand { get; set; }
        public string CarModel { get; set; }
        public string LicensePlate { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }
        public int TotalDays { get; set; }
        public decimal TotalPrice { get; set; }

        public string Status { get; set; }

        public int PickupLocationId { get; set; }
        public string PickupLocation { get; set; }

        public int DropoffLocationId { get; set; }
        public string DropoffLocation { get; set; }

        public List<PaymentDto> Payments { get; set; }
        public ContractDto? Contract { get; set; }
        public List<ReservationStatusHistoryDto> StatusHistory { get; set; }
    }

}
