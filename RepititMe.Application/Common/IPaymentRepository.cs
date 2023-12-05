using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Application.Common
{
    public interface IPaymentRepository
    {
        Task ConfirmPayment(long telegramId, double value);
    }
}
