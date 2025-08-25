using MediatR;
using RentCar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Features.Audit.Queries
{
    public record GetAuditLogsQuery(DateTime? From, DateTime? To, string? UserId, string? EntityName)
            : IRequest<List<AuditLog>>;
}
