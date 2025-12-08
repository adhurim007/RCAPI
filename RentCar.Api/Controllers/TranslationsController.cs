using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RentCar.Domain.Entities;
using RentCar.Persistence;

namespace RentCar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TranslationsController : ControllerBase
    {
        private readonly RentCarDbContext _context;
        private readonly IMemoryCache _cache;

        public TranslationsController(RentCarDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        [AllowAnonymous]
        [HttpGet("{langCode}")]
        public async Task<IActionResult> GetByLanguage(string langCode)
        {
            var translations = await (
                from t in _context.Translation
                join l in _context.Language on t.LanguageId equals l.Id
                where l.Code == langCode
                select new { t.Key, t.Value }
            ).ToListAsync();

            var dict = translations.ToDictionary(x => x.Key, x => x.Value);

            return Ok(dict);
        }

         
        [HttpGet("admin/by-language/{languageId}")]
        public async Task<IActionResult> GetAdminByLanguage(int languageId)
        {
            var translations = await _context.Translation
                .Where(t => t.LanguageId == languageId)
                .Select(t => new {
                    t.Id,
                    t.LanguageId,
                    t.Key,
                    t.Value
                })
                .ToListAsync();

            return Ok(translations);
        }
         
        // List translations for a language (for admin grid)
        [HttpGet("list/{languageId:int}")] 
        public async Task<IActionResult> GetList(int languageId, [FromQuery] string? search = null)
        {
            var query = _context.Translation
                .Where(t => t.LanguageId == languageId);

            if (!string.IsNullOrWhiteSpace(search))
            {
                search = search.Trim();
                query = query.Where(t => t.Key.Contains(search) || t.Value.Contains(search));
            }

            var list = await query
                .OrderBy(t => t.Key)
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost]
      
        public async Task<IActionResult> Create([FromBody] Translation translation)
        {
            _context.Translation.Add(translation);
            await _context.SaveChangesAsync();
             
            var langCode = await _context.Language
                .Where(l => l.Id == translation.LanguageId)
                .Select(l => l.Code)
                .FirstOrDefaultAsync();

            if (langCode != null)
            {
                _cache.Remove($"translations-{langCode}");
            }

            return Ok(translation);
        }

        [HttpPut("{id:int}")] 
        public async Task<IActionResult> Update(int id, [FromBody] Translation translation)
        {
            if (id != translation.Id) return BadRequest("Id mismatch");

            _context.Entry(translation).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            var langCode = await _context.Language
                .Where(l => l.Id == translation.LanguageId)
                .Select(l => l.Code)
                .FirstOrDefaultAsync();

            if (langCode != null)
            {
                _cache.Remove($"translations-{langCode}");
            }

            return NoContent();
        }

        [HttpDelete("{id:int}")] 
        public async Task<IActionResult> Delete(int id)
        {
            var translation = await _context.Translation.FindAsync(id);
            if (translation == null) return NotFound();

            _context.Translation.Remove(translation);
            await _context.SaveChangesAsync();

            var langCode = await _context.Language
                .Where(l => l.Id == translation.LanguageId)
                .Select(l => l.Code)
                .FirstOrDefaultAsync();

            if (langCode != null)
            {
                _cache.Remove($"translations-{langCode}");
            }

            return NoContent();
        }
    }

}
