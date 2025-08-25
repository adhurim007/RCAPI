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

        // Admin dashboard summary
        [HttpGet("admin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await _mediator.Send(new GetAdminDashboardQuery());
            return Ok(result);
        }

        // Business dashboard summary
        [HttpGet("business/{businessId}")]
        [Authorize(Roles = "Business")]
        public async Task<IActionResult> GetBusinessDashboard(int businessId)
        {
            var result = await _mediator.Send(new GetBusinessDashboardQuery(businessId));
            return Ok(result);
        }

        // 📊 Reservations per month
        [HttpGet("reservations-per-month")]
        public async Task<IActionResult> GetReservationsPerMonth([FromQuery] int? businessId)
        {
            var stats = await _mediator.Send(new GetReservationsPerMonthQuery(businessId));
            return Ok(stats);
        }

        // 💰 Income per month
        [HttpGet("income-per-month")]
        public async Task<IActionResult> GetIncomePerMonth([FromQuery] int? businessId)
        {
            var stats = await _mediator.Send(new GetIncomePerMonthQuery(businessId));
            return Ok(stats);
        }
    }
}
