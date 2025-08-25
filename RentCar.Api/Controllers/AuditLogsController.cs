using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.Audit.Queries;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")] // Only SuperAdmin can view audit logs
    public class AuditLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuditLog>>> GetAuditLogs(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? userId,
            [FromQuery] string? entityName)
        {
            var logs = await _mediator.Send(new GetAuditLogsQuery(from, to, userId, entityName));
            return Ok(logs);
        }
    }
}
