using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Localization
{
    public interface ILocalizationService
    {
        Task<Dictionary<string, string>> GetTranslationsAsync(string langCode);
        Task AddOrUpdateAsync(string langCode, string key, string value);
    }
}
