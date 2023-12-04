using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Domain.Object.Orders
{
    public class NewOrderObject
    {
        public long TelegramIdTeacher { get; set; }
        public long TelegramIdStudent { get; set; }
        public string? Description { get; set; }
    }
}
