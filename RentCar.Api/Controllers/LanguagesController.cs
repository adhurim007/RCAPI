using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Persistence;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
    public class LanguagesController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public LanguagesController(RentCarDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Language.OrderBy(l => l.Id).ToListAsync());
        }

        [AllowAnonymous]
        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            return Ok(await _context.Language.Where(l => l.IsActive).OrderBy(l => l.Id).ToListAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var lang = await _context.Language.FindAsync(id);
            return lang == null ? NotFound() : Ok(lang);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Language language)
        {
            _context.Language.Add(language);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = language.Id }, language);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Language language)
        {
            if (id != language.Id) return BadRequest("Id mismatch");
            _context.Entry(language).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lang = await _context.Language.FindAsync(id);
            if (lang == null) return NotFound();

            _context.Language.Remove(lang);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }

}
