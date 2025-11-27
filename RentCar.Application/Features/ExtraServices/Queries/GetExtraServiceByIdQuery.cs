using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.ExtraServices.Queries
{
    public class GetExtraServiceByIdQuery : IRequest<ExtraService>
    {
        public int Id { get; set; }
    }
}
