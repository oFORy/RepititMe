using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using RepititMe.Application.Services.Reports.Commands;
using RepititMe.Application.Services.Reports.Queries;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Orders;
using RepititMe.Domain.Object.Reports;
using System.Runtime.CompilerServices;

namespace RepititMe.Api.Controllers
{
    [ApiController]
    [EnableCors("enablecorspolicy")]
    public class ReportController : Controller
    {
        private readonly IReportCommandService _commandService;
        private readonly IReportQueryService _queryService;
        public ReportController(IReportCommandService reportCommandService, IReportQueryService reportQueryService)
        {
            _commandService = reportCommandService;
            _queryService = reportQueryService;
        }

        [HttpPost("Api/Report/NewReport")]
        public async Task<bool> NewReports([FromBody] NewReportsObject newReportsObject)
        {
            return await _commandService.NewReport(newReportsObject);
        }


        [HttpGet("Api/Report/ShowAll")]
        public async Task<List<Report>> ShowAllReports(long telegramId, int orderId)
        {
            return await _queryService.ShowAllReports(telegramId, orderId);
        }
    }
}
