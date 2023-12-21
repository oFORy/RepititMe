using RepititMe.Domain.Entities.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Common
{
    public interface IPaymentRepository
    {
        Task ConfirmPayment(int orderId, string paymentId);
        Task<bool> CheckPayment(int orderId);
        Task DeletePayment(int orderId, string paymentId);
        Task<bool> Createpayment(int orderId, string paymentId, double value);
        Task<PaymentStatus> GetPaymentData(int orderId);
    }
}
