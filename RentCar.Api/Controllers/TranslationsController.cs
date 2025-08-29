using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentCar.Persistence;

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

        [HttpGet("{langCode}")]
        public async Task<IActionResult> GetTranslations(string langCode)
        {
            var lang = await _context.Language
                .FirstOrDefaultAsync(l => l.Code == langCode && l.IsActive);

            if (lang == null) return NotFound("Language not found");

            var translations = await _context.Translation
                .Where(t => t.LanguageId == lang.Id)
                .ToDictionaryAsync(t => t.Key, t => (object)t.Value);

            return Ok(translations);
        }
    }

}
