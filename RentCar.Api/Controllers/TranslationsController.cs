using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Persistence;
using RentCar.Domain.Entities;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationsController : ControllerBase
    {
        private readonly RentCarDbContext _context;

        public TranslationsController(RentCarDbContext context)
        {
            _context = context;
        }

        // 🔹 For Transloco (Angular)
        [HttpGet("{langCode}")]
        [AllowAnonymous] // clients need this
        public async Task<IActionResult> GetByLanguage(string langCode)
        {
            var lang = await _context.Language.FirstOrDefaultAsync(l => l.Code == langCode && l.IsActive);
            if (lang == null) return NotFound("Language not found");

            var dict = await _context.Translation
                .Where(t => t.LanguageId == lang.Id)
                .ToDictionaryAsync(t => t.Key, t => t.Value);

            return Ok(dict);
        }

        // 🔹 For Admin management
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] Translation translation)
        {
            _context.Translation.Add(translation);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetByLanguage), new { langCode = translation.Language.Code }, translation);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Update(int id, [FromBody] Translation translation)
        {
            if (id != translation.Id) return BadRequest("Id mismatch");

            _context.Entry(translation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var translation = await _context.Translation.FindAsync(id);
            if (translation == null) return NotFound();

            _context.Translation.Remove(translation);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
