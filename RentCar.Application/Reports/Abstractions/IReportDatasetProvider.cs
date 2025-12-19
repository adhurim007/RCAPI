using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Abstractions
{
    public interface IReportDatasetProvider
    {
        /// <summary>
        /// Emri i dataset-it (DUHET të përputhet me RDL)
        /// </summary>
        string DatasetName { get; }

        /// <summary>
        /// Kodet e raporteve që ky provider mbështet
        /// </summary>
        IReadOnlyCollection<string> SupportedReportCodes { get; }

        /// <summary>
        /// Ndërton DataTable për raportin
        /// </summary>
        DataTable Build(IDictionary<string, object?> parameters);
    }
}
