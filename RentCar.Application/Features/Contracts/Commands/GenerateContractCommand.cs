using MediatR;
using RentCar.Application.DTOs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Contracts.Commands
{
    public record GenerateContractCommand(int ReservationId) : IRequest<int>;

}
