using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Core
{
    public class ReportRequest
    {
        public string ReportCode { get; set; } = string.Empty;
        public Dictionary<string, object?> Parameters { get; set; } = new();
    }
}
