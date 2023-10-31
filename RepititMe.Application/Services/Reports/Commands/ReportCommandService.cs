using RepititMe.Application.Services.Reports.Common;
using RepititMe.Domain.Object.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reports.Commands
{
    public class ReportCommandService : IReportCommandService
    {
        private readonly IReportRepository _reportRepository;
        public ReportCommandService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<bool> NewReport(NewReportsObject newReportsObject)
        {
            return await _reportRepository.NewReport(newReportsObject);
        }
    }
}
