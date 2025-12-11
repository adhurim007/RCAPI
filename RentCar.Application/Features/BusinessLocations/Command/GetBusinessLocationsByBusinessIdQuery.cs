using MediatR;
using RentCar.Application.DTOs.BusinessLocationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Command
{
    public record GetBusinessLocationsByBusinessIdQuery(int BusinessId) : IRequest<List<BusinessLocationDto>>;
}
