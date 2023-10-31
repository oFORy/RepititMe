using RepititMe.Domain.Object.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Services.Reports.Commands
{
    public interface IReportCommandService
    {
        Task<bool> NewReport(NewReportsObject newReportsObject);
    }
}
