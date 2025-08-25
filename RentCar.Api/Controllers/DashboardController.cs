using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.Dashboard.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ADMIN Dashboard
        [HttpGet("admin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await _mediator.Send(new GetAdminDashboardQuery());
            return Ok(result);
        }

        // BUSINESS Dashboard
        [HttpGet("business/{businessId}")]
        [Authorize(Roles = "Business")]
        public async Task<IActionResult> GetBusinessDashboard(int businessId)
        {
            var result = await _mediator.Send(new GetBusinessDashboardQuery(businessId));
            return Ok(result);
        }
    }
}
