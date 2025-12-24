using MediatR; 
using RentCar.Application.Reports.Engine; 
using RentCar.Application.Reports.Queries.RentCar.Application.Reports.Queries;
 
namespace RentCar.Application.Reports.Handlers
{
    public sealed class GenerateReportQueryHandler
       : IRequestHandler<GenerateReportQuery, byte[]>
    {
        private readonly ReportEngine _engine;

        public GenerateReportQueryHandler(ReportEngine engine)
        {
            _engine = engine;
        }

        public async Task<byte[]> Handle(
            GenerateReportQuery request,
            CancellationToken cancellationToken)
        {
            return await _engine.GeneratePdfAsync(
                request.ReportCode,
                request.Parameters,
                cancellationToken);
        }
    }

}
