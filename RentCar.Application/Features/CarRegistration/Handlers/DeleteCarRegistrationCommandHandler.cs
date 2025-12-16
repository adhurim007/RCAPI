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
    public class DeleteCarRegistrationCommandHandler
    : IRequestHandler<DeleteCarRegistrationCommand, bool>
    {
        private readonly RentCarDbContext _context;

        public DeleteCarRegistrationCommandHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(
            DeleteCarRegistrationCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.CarRegistrations
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
                return false;

            _context.CarRegistrations.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
