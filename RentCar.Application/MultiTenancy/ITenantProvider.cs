using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.MultiTenancy
{
    public interface ITenantProvider
    {
        int? GetBusinessId();
        bool IsSuperAdmin();
    }

    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TenantProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int? GetBusinessId()
        {
            var claim = _httpContextAccessor.HttpContext?.User?.FindFirst("BusinessId");
            return claim != null ? int.Parse(claim.Value) : null;
        }

        public bool IsSuperAdmin()
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole("SuperAdmin") ?? false;
        }
    }
}
