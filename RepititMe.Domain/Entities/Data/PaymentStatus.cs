using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Entities.Data
{
    public class PaymentStatus
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentId { get; set; }
        public double Value { get; set; }
        public bool WaitingPayment { get; set; } = true;
        public bool Paid { get; set; } = false;
    }
}
