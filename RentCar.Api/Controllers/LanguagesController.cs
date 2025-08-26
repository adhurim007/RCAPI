using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Persistence;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "SuperAdmin")] // only superadmin can manage languages
    public class LanguagesController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public LanguagesController(RentCarDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var langs = await _context.Language.ToListAsync();
            return Ok(langs);
        }

        [HttpGet("{id}")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Language language)
        {
            if (id != language.Id) return BadRequest("Id mismatch");
            _context.Entry(language).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
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
