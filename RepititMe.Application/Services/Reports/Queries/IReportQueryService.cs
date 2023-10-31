using RepititMe.Domain.Entities.Weights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reports.Queries
{
    public interface IReportQueryService
    {
        Task<List<Report>> ShowAllReports(int telegramId, int orderId);
    }
}
