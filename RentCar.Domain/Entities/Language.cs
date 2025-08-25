using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class Language
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;  // e.g. 'en', 'fr', 'sq'
        public string Name { get; set; } = string.Empty;  // e.g. 'English'
        public bool IsActive { get; set; } = true;

        // 🔗 Navigation
        public ICollection<Translation> Translations { get; set; } = new List<Translation>();
    }

    public class Translation
    {
        public int Id { get; set; }
        public int LanguageId { get; set; }
        public string Key { get; set; } = string.Empty;    // e.g. 'dashboard.title'
        public string Value { get; set; } = string.Empty;  // e.g. 'Dashboard'

        // 🔗 Navigation
        public Language Language { get; set; }
    }
}
