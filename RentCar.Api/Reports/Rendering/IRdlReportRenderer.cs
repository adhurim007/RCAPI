using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Rendering
{
    public interface IRdlReportRenderer
    {
        byte[] RenderToPdf(string rdlPath, DataSet dataSet);
    }

}
