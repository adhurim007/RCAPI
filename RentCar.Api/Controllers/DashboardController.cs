using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentCar.Application.Features.Dashboard.Queries;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // bazë
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ====================== BUSINESS ======================
        // Summary cards + charts (last 12 months)
        [HttpGet("business")] 
        public async Task<IActionResult> GetBusinessDashboard()
        {
            var result = await _mediator.Send(new GetBusinessDashboardQuery());
            return Ok(result);
        }

        // Reservations per month (nëse do endpoint veç)
        [HttpGet("business/reservations-per-month")] 
        public async Task<IActionResult> GetReservationsPerMonth()
        {
            var stats = await _mediator.Send(new GetReservationsPerMonthQuery());
            return Ok(stats);
        }

        // Income per month (nëse do endpoint veç)
        [HttpGet("business/income-per-month")] 
        public async Task<IActionResult> GetIncomePerMonth()
        {
            var stats = await _mediator.Send(new GetIncomePerMonthQuery());
            return Ok(stats);
        }

        // ====================== ADMIN ======================
        [HttpGet("admin")] 
        public async Task<IActionResult> GetAdminDashboard()
        {
            var result = await _mediator.Send(new GetAdminDashboardQuery());
            return Ok(result);
        }
    }
}
