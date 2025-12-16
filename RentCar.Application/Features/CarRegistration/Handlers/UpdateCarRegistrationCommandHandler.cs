using MediatR;
using RentCar.Application.Features.CarRegistration.Command;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarRegistration.Handlers
{
    public class UpdateCarRegistrationCommandHandler
    : IRequestHandler<UpdateCarRegistrationCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public UpdateCarRegistrationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            UpdateCarRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.CarRegistrations
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                return false;

            entity.CarId = request.CarId;
            entity.RegistrationNumber = request.RegistrationNumber;
            entity.IssuedDate = request.IssuedDate;
            entity.ExpiryDate = request.ExpiryDate;
            entity.Cost = request.Cost;
            entity.InsuranceCompany = request.InsuranceCompany;
            entity.InsuranceExpiryDate = request.InsuranceExpiryDate;
            entity.DocumentUrl = request.DocumentUrl;
            entity.Notes = request.Notes;

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
