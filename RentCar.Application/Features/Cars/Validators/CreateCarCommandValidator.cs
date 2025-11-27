using FluentValidation;
using RentCar.Application.Features.Cars.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Cars.Validators
{
    public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
    {
        public CreateCarCommandValidator()
        {
            RuleFor(x => x.LicensePlate).NotEmpty().MaximumLength(15);
            RuleFor(x => x.Color).NotEmpty().MaximumLength(30);
            RuleFor(x => x.DailyPrice).GreaterThan(0);
            //RuleFor(x => x.ImageUrl).NotEmpty().MaximumLength(250);
            RuleFor(x => x.Description).MaximumLength(500);
            //RuleFor(x => x.BusinessId).NotEmpty();
        }
    }
}
