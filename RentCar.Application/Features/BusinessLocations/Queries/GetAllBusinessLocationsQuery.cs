using MediatR;
using RentCar.Application.DTOs.BusinessLocationDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.BusinessLocations.Queries
{
    public class GetAllBusinessLocationsQuery : IRequest<List<BusinessLocationDto>>
    {
    }
}
