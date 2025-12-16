using MediatR;
using RentCar.Application.Features.CarRegistration.Command;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarRegistration.Handlers
{
    public class CreateCarRegistrationCommandHandler
     : IRequestHandler<CreateCarRegistrationCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateCarRegistrationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(
            CreateCarRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.CarRegistration()
            {
                CarId = request.CarId,
                BusinessId = request.BusinessId,
                RegistrationNumber = request.RegistrationNumber,
                IssuedDate = request.IssuedDate,
                ExpiryDate = request.ExpiryDate,
                Cost = request.Cost,
                InsuranceCompany = request.InsuranceCompany,
                InsuranceExpiryDate = request.InsuranceExpiryDate,
                DocumentUrl = request.DocumentUrl,
                Notes = request.Notes,
                CreatedAt = DateTime.UtcNow
            };

            _context.CarRegistrations.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
