using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Commands
{
    public class UpdateReservationCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DateTime PickupDate { get; set; }
        public DateTime DropoffDate { get; set; }

        public int PickupLocationId { get; set; }
        public int DropoffLocationId { get; set; }

        public string? Notes { get; set; }
        public int ReservationStatusId { get; set; }
        public int? Discount { get; set; }

    }

}
