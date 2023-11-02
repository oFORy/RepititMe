using RepititMe.Application.Services.Reports.Common;
using RepititMe.Domain.Entities.Weights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reports.Queries
{
    public class ReportQueryService : IReportQueryService
    {
        private readonly IReportRepository _reportRepository;
        public ReportQueryService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<List<Report>> ShowAllReports(long telegramId, int orderId)
        {
            return await _reportRepository.ShowAllReports(telegramId, orderId);
        }
    }
}
