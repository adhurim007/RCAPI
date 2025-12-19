using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Abstractions
{
    public interface IReportDefinition
    {
        string Code { get; }
        string Name { get; }
        string Description { get; }
    }
}
