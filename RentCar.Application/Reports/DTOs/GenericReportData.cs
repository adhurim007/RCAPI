using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.DTOs
{
    public class GenericReportData
    {
        public List<string> Columns { get; set; } = new();
        public List<IDictionary<string, object?>> Rows { get; set; } = new();
    }
}
