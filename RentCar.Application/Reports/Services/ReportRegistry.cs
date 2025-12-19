using RentCar.Application.Reports.Abstractions;
using RentCar.Application.Reports.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Services
{
    public class ReportRegistry : IReportRegistry
    {
        private readonly Dictionary<string, IReportBuilder> _builders;
        private readonly List<IReportDefinition> _definitions;

        public ReportRegistry(IEnumerable<IReportBuilder> builders)
        {
            _builders = builders.ToDictionary(
                b => b.ReportCode,
                StringComparer.OrdinalIgnoreCase
            );

            // Lista për UI / evidencë (jo për logjikë)
            _definitions = new List<IReportDefinition>
        {
            new ReportDefinition
            {
                Code = "RESERVATION_CONTRACT",
                Name = "Reservation Contract",
                Description = "Contract generated after reservation"
            }
        };
        }

        public IReadOnlyCollection<IReportDefinition> GetAll()
            => _definitions.AsReadOnly();

        public IReportBuilder GetBuilder(string reportCode)
        {
            // 1️⃣ Exact match (nëse ndonjëherë shton builder specifik)
            if (_builders.TryGetValue(reportCode, out var builder))
                return builder;

            // 2️⃣ FALLBACK → Dynamic
            if (_builders.TryGetValue("DYNAMIC", out var dynamicBuilder))
                return dynamicBuilder;

            throw new KeyNotFoundException(
                $"No report builder found for '{reportCode}' and no dynamic fallback is registered."
            );
        }
    }

}
