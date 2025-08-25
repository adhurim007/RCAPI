using MediatR;
using RentCar.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Dashboard.Queries
{
    public record GetReservationsPerMonthQuery(int? BusinessId) : IRequest<List<MonthlyStatDto>>; 
}
