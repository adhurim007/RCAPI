using Microsoft.Reporting.NETCore;
using RentCar.Api.Reports.Rendering;
using RentCar.Application.Reports.Abstractions;
using System.Data; 
namespace RentCar.Application.Reports.Rendering
{
    public sealed class RdlReportRenderer : IReportRenderer
    {
        private readonly IWebHostEnvironment _env;

        public RdlReportRenderer(IWebHostEnvironment env)
        {
            _env = env;
        }

        public byte[] Render(string reportCode, DataSet ds)
        {
            var path = Path.Combine(
                _env.ContentRootPath,
                "Reports",
                "Reservation",
                $"{reportCode}.rdl");

            return RdlPdfRenderer.Render(path, ds);
        }
    }


}
