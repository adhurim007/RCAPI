using Microsoft.AspNetCore.Http;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System.Security.Claims;
using System.Text.Json;

namespace RentCar.Application.Auditing
{
    public interface IAuditLogService
    {
        Task LogAsync(string action, string entityName, string entityId, object? changes = null);
    }

    public class AuditLogService : IAuditLogService
    {
        private readonly RentCarDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditLogService(RentCarDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogAsync(string action, string entityName, string entityId, object? changes = null)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
            var ipAddress = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            var log = new AuditLog
            {
                UserId = userId,
                Action = action,
                EntityName = entityName,
                EntityId = entityId,
                Changes = changes != null ? JsonSerializer.Serialize(changes) : string.Empty,
                IpAddress = ipAddress,
                Timestamp = DateTime.UtcNow
            };

            _context.AuditLogs.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
