using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarRegistration.Command
{
    public record UpdateCarRegistrationCommand(
    int Id,
    int CarId,
    string RegistrationNumber,
    DateTime IssuedDate,
    DateTime ExpiryDate,
    decimal Cost,
    string? InsuranceCompany,
    DateTime? InsuranceExpiryDate,
    string? DocumentUrl,
    string? Notes
) : IRequest<bool>;
}
