using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.DTOs.Cars;
using RentCar.Application.Features.CarRegistration.Queries;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.CarRegistration.Handlers
{
    public class GetCarRegistrationByIdQueryHandler
        : IRequestHandler<GetCarRegistrationByIdQuery, CarRegistrationDto?>
    {
        private readonly RentCarDbContext _context;

        public GetCarRegistrationByIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<CarRegistrationDto?> Handle(
            GetCarRegistrationByIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.CarRegistrations
                .Where(x => x.Id == request.Id)
                .Select(x => new CarRegistrationDto
                {
                   Id= x.Id,
                   CarId= x.CarId, 
                   RegistrationNumber = x.RegistrationNumber,
                   IssuedDate = x.IssuedDate,
                   ExpiryDate = x.ExpiryDate,
                   Cost = x.Cost,
                   InsuranceCompany = x.InsuranceCompany,
                   InsuranceExpiryDate = x.InsuranceExpiryDate, 
                   Notes = x.Notes,
                   DocumentUrl = x.DocumentUrl,
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
