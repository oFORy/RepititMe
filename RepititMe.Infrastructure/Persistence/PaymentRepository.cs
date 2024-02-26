using Microsoft.EntityFrameworkCore;
using RepititMe.Application.bot.Services;
using RepititMe.Application.Common;
using RepititMe.Domain.Entities.Data;
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

        public async Task<bool> CheckPayment(int orderId)
        {
            var confPaymentOrder = await _botDbContext.PaymentStatuses.FirstOrDefaultAsync(c => c.OrderId == orderId && c.WaitingPayment);
            if (confPaymentOrder != null)
            {
                return true;
            }
            return false;
        }

        public async Task ConfirmPayment(int orderId, string paymentId)
        {
            var clearPay = await _botDbContext.PaymentStatuses
                .Where(c => c.OrderId == orderId && c.PaymentId == paymentId)
                .FirstOrDefaultAsync();

            if (clearPay != null)
            {
                var order = await _botDbContext.Orders
                    .Include(t => t.Teacher)
                    .FirstOrDefaultAsync(c => c.Id == orderId);
                if (order != null)
                {
                    order.Commission = Math.Round(order.Commission - clearPay.Value, 2);
                    order.PaidCommission += clearPay.Value;
                    await _botDbContext.SaveChangesAsync();


                    double sumOrders = await _botDbContext.Orders
                        .Where(t => t.TeacherId == order.TeacherId)
                        .SumAsync(o => o.PaidCommission);

                    var countOrders = await _botDbContext.Orders
                        .Where(t => t.TeacherId == order.TeacherId)
                        .CountAsync();

                    var newPaymentRating = sumOrders / countOrders;
                    var teacherAddRating = await _botDbContext.Teachers.Where(t => t.Id == order.TeacherId).FirstOrDefaultAsync();
                    if (teacherAddRating != null)
                    {
                        teacherAddRating.PaymentRatingFromCommission = newPaymentRating;
                        await _botDbContext.SaveChangesAsync();

                        teacherAddRating.PaymentRating = teacherAddRating.PaymentRatingFromProfile + teacherAddRating.PaymentRatingFromCommission;
                        await _botDbContext.SaveChangesAsync();
                    }

                }

                clearPay.Paid = true;
                clearPay.WaitingPayment = false;
                await _botDbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> Createpayment(int orderId, string paymentId, double value)
        {
            var newPayment = new PaymentStatus
            {
                OrderId = orderId,
                PaymentId = paymentId,
                Value = value
            };
            await _botDbContext.PaymentStatuses.AddAsync(newPayment);
            return await _botDbContext.SaveChangesAsync() > 0;
        }

        public async Task DeletePayment(int orderId, string paymentId)
        {
            var confPaymentOrder = await _botDbContext.PaymentStatuses.FirstOrDefaultAsync(c => c.OrderId == orderId && c.PaymentId == paymentId);
            if (confPaymentOrder != null)
            {
                confPaymentOrder.WaitingPayment = false;
                await _botDbContext.SaveChangesAsync();
            }
        }

        public async Task<PaymentStatus> GetPaymentData(int orderId)
        {
            return await _botDbContext.PaymentStatuses.Where(u => u.OrderId == orderId && u.WaitingPayment).OrderByDescending(c => c.Id).FirstOrDefaultAsync();
        }
    }
}
