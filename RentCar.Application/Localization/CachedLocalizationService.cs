using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using RentCar.Domain.Entities;
using RentCar.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Localization
{
    public class CachedLocalizationService : ILocalizationService
    {
        private readonly RentCarDbContext _context;
        private readonly IMemoryCache _cache;

        public CachedLocalizationService(RentCarDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<Dictionary<string, string>> GetTranslationsAsync(string langCode)
        {
            return await _cache.GetOrCreateAsync($"translations_{langCode}", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30);
                var lang = await _context.Language.FirstOrDefaultAsync(l => l.Code == langCode && l.IsActive);
                if (lang == null) return new Dictionary<string, string>();

                return await _context.Translation
                    .Where(t => t.LanguageId == lang.Id)
                    .ToDictionaryAsync(t => t.Key, t => t.Value);
            });
        }

        public async Task AddOrUpdateAsync(string langCode, string key, string value)
        {
            var lang = await _context.Language.FirstOrDefaultAsync(l => l.Code == langCode);
            if (lang == null) throw new Exception("Language not found");

            var existing = await _context.Translation.FirstOrDefaultAsync(t => t.LanguageId == lang.Id && t.Key == key);
            if (existing != null)
            {
                existing.Value = value;
            }
            else
            {
                _context.Translation.Add(new Translation { Key = key, Value = value, LanguageId = lang.Id });
            }

            await _context.SaveChangesAsync();
             
            _cache.Remove($"translations_{langCode}");
        }
    }
}
