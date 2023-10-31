using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reports.Common
{
    public interface IReportRepository
    {
        Task<List<Report>> ShowAllReports(int telegramId, int orderId);
        Task<bool> NewReport(NewReportsObject newReportsObject);
    }
}
