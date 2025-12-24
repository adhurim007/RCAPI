using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Application.Reports.Abstractions
{
    public interface IReport
    {
        string Code { get; }

        Task<DataSet> BuildDataSetAsync(
            IDictionary<string, object?> parameters,
            CancellationToken cancellationToken);
    }
}
