using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Handlers
{
    public class UpdateCarCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string LicensePlate { get; set; }
        public string Color { get; set; }
        public decimal DailyPrice { get; set; }
        public int CarModelId { get; set; }
        public Guid BusinessId { get; set; }
    }
}
