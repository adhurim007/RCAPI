using MediatR;
using RentCar.Application.DTOs.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Commands
{
    public class CreateReservationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }

        public decimal? Discount { get; set; }
        public string? Notes { get; set; }
        public string? PersonalNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        public List<ReservationExtraServiceDto> ExtraServices { get; set; }
            = new List<ReservationExtraServiceDto>();
    }
}
