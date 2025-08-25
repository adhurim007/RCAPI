using MediatR;
using Microsoft.EntityFrameworkCore;
using RentCar.Application.Features.Audit.Queries;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Audit.Handlers
{
    public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, List<AuditLog>>
    {
        private readonly RentCarDbContext _context;

        public GetAuditLogsQueryHandler(RentCarDbContext context)
        {
            _context = context;
        }

        public async Task<List<AuditLog>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.AuditLogs.AsQueryable();

            if (request.From.HasValue)
                query = query.Where(x => x.Timestamp >= request.From.Value);

            if (request.To.HasValue)
                query = query.Where(x => x.Timestamp <= request.To.Value);

            if (!string.IsNullOrEmpty(request.UserId))
                query = query.Where(x => x.UserId == request.UserId);

            if (!string.IsNullOrEmpty(request.EntityName))
                query = query.Where(x => x.EntityName == request.EntityName);

            return await query.OrderByDescending(x => x.Timestamp).ToListAsync(cancellationToken);
        }
    }
}
