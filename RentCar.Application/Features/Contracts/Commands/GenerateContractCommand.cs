using MediatR;
using RentCar.Application.DTOs.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Contracts.Commands
{
    public class GenerateContractCommand : IRequest<int>
    {
        public int ReservationId { get; set; }
        public string GeneratedBy { get; set; } // Business User who generated the contract
    }

}
