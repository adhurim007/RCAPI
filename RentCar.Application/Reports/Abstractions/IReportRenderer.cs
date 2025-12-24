using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Abstractions
{
    public interface IReportRenderer
    {
        byte[] Render(string reportCode, DataSet dataSet);
    }

}
