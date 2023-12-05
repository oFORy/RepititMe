using Microsoft.EntityFrameworkCore;
using RepititMe.Application.bot.Services;
using RepititMe.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure.Persistence
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BotDbContext _botDbContext;
        public PaymentRepository(BotDbContext context)
        {
            _botDbContext = context;
        }

        public async Task ConfirmPayment(long telegramId, double value)
        {
            var confPaymentOrder =  await _botDbContext.Orders.Include(u => u.Teacher).ThenInclude(t => t.User).FirstOrDefaultAsync(c => c.Teacher.User.TelegramId == telegramId);
            if (confPaymentOrder != null)
            {
                confPaymentOrder.Commission -= value;
            }
        }
    }
}
