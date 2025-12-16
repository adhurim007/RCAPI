using MediatR;
using RentCar.Domain.Entities;
using RentCar.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarService.Commands
{
    public record CreateCarServiceCommand(
        int CarId,
        int BusinessId,
        CarServiceType ServiceType,
        DateTime ServiceDate,
        int? Mileage,
        decimal Cost,
        string? ServiceCenter,
        DateTime? NextServiceDate,
        int? NextServiceMileage,
        string? Notes
    ) : IRequest<int>;
}
