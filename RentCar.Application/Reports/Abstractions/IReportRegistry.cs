using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Abstractions
{
    public interface IReportRegistry
    {
        IReadOnlyCollection<IReportDefinition> GetAll();
        IReportBuilder GetBuilder(string reportCode);
    }
}
