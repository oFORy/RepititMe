using Microsoft.EntityFrameworkCore;
using RepititMe.Application.Services.Reports.Common;
using RepititMe.Domain.Entities.Weights;
using RepititMe.Domain.Object.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class ReportRepository : IReportRepository
    {
        private readonly BotDbContext _botDbContext;
        public ReportRepository(BotDbContext botDbContext)
        {
            _botDbContext = botDbContext;
        }

        public async Task<bool> NewReport(NewReportsObject newReportsObject)
        {
            var newReport = new Report()
            {
                Description = newReportsObject.Description != null ? newReportsObject.Description : null,
                Price = newReportsObject.Price,
                DateTime = newReportsObject.DateTimeReport,
                OrderId = newReportsObject.OrderId
            };
            await _botDbContext.Reports.AddAsync(newReport);
            if (await _botDbContext.SaveChangesAsync() == 0)
                return false;

            var addCommission = await _botDbContext.Reports.Include(r => r.Order).Where(o => o.Order.Id == newReportsObject.OrderId).FirstOrDefaultAsync();
            if (addCommission != null)
            {
                addCommission.Order.Commission += newReportsObject.Price * 0.1;
                return await _botDbContext.SaveChangesAsync() > 0;
            }
            return false;
        }

        public async Task<List<Report>> ShowAllReports(int telegramId, int orderId)
        {
            return await _botDbContext.Reports
                .Where(i => i.OrderId == orderId)
                .ToListAsync(); ;
        }
    }
}
