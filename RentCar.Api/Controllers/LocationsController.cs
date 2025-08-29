using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Persistence;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]   // 👈 This ensures URL = /api/locations/...
    public class LocationsController : ControllerBase
    {
        private readonly RentCarDbContext _db;

        public LocationsController(RentCarDbContext db)
        {
            _db = db;
        }

        // GET: /api/locations/states
        [HttpGet("states")]
        public async Task<IActionResult> GetStates()
        {
            var states = await _db.States
                .Select(s => new { s.Id, s.Name })
                .ToListAsync();

            return Ok(states);
        }

        // GET: /api/locations/cities/{stateId}
        [HttpGet("cities/{stateId}")]   
        public async Task<IActionResult> GetCities(int stateId)
        {
            var cities = await _db.Cities
                .Where(c => c.StateId == stateId)
                .Select(c => new { c.Id, c.Name })
                .ToListAsync();

            return Ok(cities);
        }
    }
}
