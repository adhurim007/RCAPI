using MediatR;
using RentCar.Application.DTOs.VehicleInspection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.VehicleInspection.Queries
{
    public record GetInspectionByIdQuery(int Id) : IRequest<VehicleInspectionDto>;

}
