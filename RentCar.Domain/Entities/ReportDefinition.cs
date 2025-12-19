using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Domain.Entities
{
    public class ReportDefinition
    {
        public int Id { get; set; }

        /// <summary>
        /// Unik – duhet të përputhet me ReportCode nga IReportBuilder
        /// p.sh. SALES_REPORT, RESERVATION_INVOICE
        /// </summary>
        public string Code { get; set; } = null!;

        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        /// <summary>
        /// Finance, Vehicles, Documents, Operations, etj.
        /// </summary>
        public string? Category { get; set; }

        /// <summary>
        /// Shfaqet në Report Center (forma qendrore)
        /// </summary>
        public bool IsMain { get; set; }

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
