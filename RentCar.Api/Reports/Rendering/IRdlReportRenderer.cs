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
        /// <summary>
        /// Renders an RDL report to PDF and saves it to disk.
        /// Returns the full file path.
        /// </summary>
        string RenderToPdfAndSave(
            string rdlPath,
            DataSet dataSet,
            string outputDirectory,
            string fileName
        );
    }

}
