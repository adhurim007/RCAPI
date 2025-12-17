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
    public class GetCarRegistrationsByBusinessIdQueryHandler
        : IRequestHandler<GetCarRegistrationsByBusinessIdQuery, List<CarRegistrationDto>>
    {
        private readonly RentCarDbContext _context;

        public GetCarRegistrationsByBusinessIdQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarRegistrationDto>> Handle(
            GetCarRegistrationsByBusinessIdQuery request,
            CancellationToken cancellationToken)
        {
            return await _context.CarRegistrations
                .AsNoTracking()
                .Where(x => x.BusinessId == request.BusinessId)
                .Select(x => new CarRegistrationDto
                {
                    Id = x.Id,
                    CarId = x.CarId,
                    LicensePlate = x.Car != null ? x.Car.LicensePlate : "", 
                    RegistrationNumber = x.RegistrationNumber,
                    IssuedDate = x.IssuedDate,
                    ExpiryDate = x.ExpiryDate, 
                    Cost = x.Cost,
                    InsuranceCompany = x.InsuranceCompany,
                    InsuranceExpiryDate = x.InsuranceExpiryDate,
                    Notes = x.Notes
                })
                .OrderByDescending(x => x.ExpiryDate)
                .ToListAsync(cancellationToken);
        }
    }
}
