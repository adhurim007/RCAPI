using MediatR;
using RentCar.Application.Features.ExtraServices.Commands;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.ExtraServices.Handlers
{
    public class CreateExtraServiceCommandHandler : IRequestHandler<CreateExtraServiceCommand, int>
    {
        private readonly RentCarDbContext _context;

        public CreateExtraServiceCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateExtraServiceCommand request, CancellationToken cancellationToken)
        {
            var entity = new ExtraService
            {
                Name = request.Name,
                PricePerDay = request.PricePerDay
            };

            _context.ExtraServices.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
