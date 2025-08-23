using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Reservations.Validators
{
    public interface IReservationValidator
    {
        Task<(bool IsValid, string ErrorMessage)> ValidateAsync(Reservation reservation);
    }
}
